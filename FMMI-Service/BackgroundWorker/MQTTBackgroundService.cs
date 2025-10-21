using FMMI_Domain;
using FMMI_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.TopicTemplate;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace FMMI_Service.BackgroundWorker;

public class MQTTBackgroundService : BackgroundService
{
    private readonly IMongoDatabase _dbConnection;
    private readonly IMongoCollection<Data> _dataCollection;
    private readonly IMqttClient _mqttClient;
    private readonly IServiceScopeFactory _scopeFactory;

    private static readonly MqttTopicTemplate _historyDataTemp = new("device/+/data/#");
    private static readonly MqttTopicTemplate _AlarmTopicTemp = new("device/+/alarm/#");

    private readonly MqttClientOptions _mqttClientOptions;

    public MQTTBackgroundService(IMongoDatabase database,IServiceScopeFactory scopeFactory)
    {
        _dbConnection = database;
        _dataCollection = _dbConnection.GetCollection<Data>("Telemetri");
        _scopeFactory = scopeFactory;

        MqttFactory mqttFactory = new MqttFactory();
        _mqttClient = mqttFactory.CreateMqttClient();

        _mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("49987f455bc94d2183a5075a9fa78344.s1.eu.hivemq.cloud")
            .WithCredentials("Worker", "fMMIWORKER1234")
            .WithTlsOptions(_ => _.UseTls())
            .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V311)
            .WithCleanSession()
            .WithKeepAlivePeriod(new TimeSpan(600000000))
            .Build();

        _mqttClient.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;

        _mqttClient.DisconnectedAsync += HandleDisconnectedAsync;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("MQTT Background Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            if (!_mqttClient.IsConnected)
            {
                try
                {
                    await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);

                    if (_mqttClient.IsConnected)
                    {
                        await SubscribeToTopicsAsync(stoppingToken);
                    }
                }
                catch (OperationCanceledException){break;}
                catch (Exception ex)
                {
                    Console.WriteLine($"MQTT connection failed: {ex.Message}. Retrying in 5 seconds...");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }

        Console.WriteLine("MQTT Background Service is stopping.");
    }

    private async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs e)
    {
        if (e.ClientWasConnected)
        {
            Console.WriteLine("MQTT Disconnected unexpectedly. Attempting to reconnect...");
        }
        
    }

    private async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        var scope = _scopeFactory.CreateScope();
        FMMIContext dbContext = scope.ServiceProvider.GetRequiredService<FMMIContext>();
        try
        {
            var topic = e.ApplicationMessage.Topic;
            var payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
            Console.WriteLine($"Received: {topic} => {payload}");

            if (!string.IsNullOrEmpty(payload))
            {
                if (topic.Contains("data"))
                {
                    Data data = JsonSerializer.Deserialize<Data>(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                    if (data != null)
                    {
                        //await _dataCollection.InsertOneAsync(data);
                        try
                        {
                            DateTime parsedDate = DateTime.ParseExact(data.Date, "dddd, yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            DateTime utcDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
                            data.Dates = utcDate;
                            await dbContext.TelemetriData.AddAsync(data);
                            int rows = await dbContext.SaveChangesAsync();
                            if (rows > 0)
                                Console.WriteLine("Saved tele data on portgresql");
                        }
                        finally
                        {
                            scope.Dispose();
                        }
                    }
                }
                else if(topic.Contains("alarm"))
                {
                    AlarmLogs alarmlog = JsonSerializer.Deserialize<AlarmLogs>(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                    if (alarmlog is not null)
                    {
                        try
                        {
                            AlarmLogs foundAlarmLog = dbContext.AlarmLogs.Where(x => x.AlarmId == alarmlog.AlarmId).OrderByDescending(x => x.Dates).FirstOrDefault();
                            if (foundAlarmLog is null && alarmlog.NewAlarm)
                            {
                                DateTime parsedDate = DateTime.ParseExact(alarmlog.Date, "dddd, yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                DateTime utcDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
                                alarmlog.Dates = utcDate;
                                await dbContext.AlarmLogs.AddAsync(alarmlog);
                                int rows = await dbContext.SaveChangesAsync();
                                if (rows > 0)
                                    Console.WriteLine("Saved alarmlog data on portgresql");
                                return;
                            }
                            if (foundAlarmLog is null)
                            {
                                return;
                            }
                            if (foundAlarmLog.NewAlarm && alarmlog.NewAlarm)
                            {
                                return;
                            }
                            if (!foundAlarmLog.NewAlarm && !alarmlog.NewAlarm)
                            {
                                return;
                            }
                            if (foundAlarmLog.NewAlarm && !alarmlog.NewAlarm)
                            {
                                foundAlarmLog.NewAlarm = alarmlog.NewAlarm;
                                await dbContext.AlarmLogs.Where(x => x.Id == foundAlarmLog.Id).ExecuteUpdateAsync(x => x.SetProperty(x => x.NewAlarm, false));
                                return;
                            }
                            else 
                            {
                                if (topic.Contains("status") && alarmlog.NewAlarm)
                                {
                                    alarmlog.Dates = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                                }
                                else
                                {
                                    DateTime parsedDate = DateTime.ParseExact(alarmlog.Date, "dddd, yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                    DateTime utcDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
                                    alarmlog.Dates = utcDate;
                                }
                                await dbContext.AlarmLogs.AddAsync(alarmlog);
                                int rows = await dbContext.SaveChangesAsync();
                                if (rows > 0)
                                    Console.WriteLine("Saved alarmlog data on portgresql");
                            }
                         }
                        finally
                        {
                            scope.Dispose();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"Warning: Failed to deserialize MQTT message payload");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing MQTT message: {ex.Message}");
        }
    }

    private async Task SubscribeToTopicsAsync(CancellationToken stoppingToken)
    {
        MqttFactory mqttFactory = new MqttFactory();

        MqttClientSubscribeOptions mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
            .WithTopicFilter(f => f.WithTopicTemplate(_historyDataTemp).WithAtLeastOnceQoS())
             .WithTopicFilter(f => f.WithTopicTemplate(_AlarmTopicTemp).WithAtLeastOnceQoS())
            .Build();

        await _mqttClient.SubscribeAsync(mqttSubscribeOptions, stoppingToken);
        Console.WriteLine("Subscribed to temperature and humidity topics.");
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        if (_mqttClient.IsConnected)
        {
            await _mqttClient.DisconnectAsync(new MqttClientDisconnectOptions { Reason = (MqttClientDisconnectOptionsReason)MqttClientDisconnectReason.NormalDisconnection }, stoppingToken);
        }
        await base.StopAsync(stoppingToken);
    }
}
