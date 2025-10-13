using FMMI_Domain.Entities;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.TopicTemplate;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace FMMI_Service.Services.APIService.BackgroundWorker
{
    public class MQTTBackgroundService : BackgroundService
    {
        private readonly IMongoDatabase _dbConnection;
        private readonly IMongoCollection<TelemetriData> _telemetryCollection;
        private readonly IMqttClient _mqttClient;

        private static readonly MqttTopicTemplate _historyDataTemp = new("device/esp32/data/temperature");
        private static readonly MqttTopicTemplate _historyDataHumidity = new("device/esp32/data/humidity");
        private static readonly MqttTopicTemplate _realTimeDataTemp = new("device/esp32/realtime/temperature");
        private static readonly MqttTopicTemplate _realTimeDataHumidity = new("device/esp32/realtime/humidity");

        private readonly MqttClientOptions _mqttClientOptions;

        public MQTTBackgroundService(IMongoDatabase database)
        {
            _dbConnection = database;
            _telemetryCollection = _dbConnection.GetCollection<TelemetriData>("Telemetri");

            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            _mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("49987f455bc94d2183a5075a9fa78344.s1.eu.hivemq.cloud")
                .WithCredentials("Worker", "fMMIWORKER1234")
                .WithTlsOptions(_ => _.UseTls())
                //.WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V311)
                .WithCleanSession()
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
                        // Vent før genforsøg
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
            try
            {
                Console.WriteLine($"Received message on topic: {e.ApplicationMessage.Topic}");
                TelemetriData? telemetry = JsonSerializer.Deserialize<TelemetriData>(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));

                if (telemetry != null)
                {
                    await _telemetryCollection.InsertOneAsync(telemetry);
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
            var mqttFactory = new MqttFactory();

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => f.WithTopic(_historyDataTemp.ToString()).WithAtLeastOnceQoS())
                .WithTopicFilter(f => f.WithTopic(_historyDataHumidity.ToString()).WithAtLeastOnceQoS())
                //.WithTopicFilter(f => f.WithTopic(_realTimeDataTemp.ToString()).WithAtLeastOnceQoS())
                //.WithTopicFilter(f => f.WithTopic(_realTimeDataHumidity.ToString()).WithAtLeastOnceQoS())
                .Build();

            await _mqttClient.SubscribeAsync(mqttSubscribeOptions, stoppingToken);
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
}
