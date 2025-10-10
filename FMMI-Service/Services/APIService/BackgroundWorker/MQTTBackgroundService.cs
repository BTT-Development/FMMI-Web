using FMMI_Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MQTTnet;
using MQTTnet.Extensions.TopicTemplate;
using System.Text;
using System.Text.Json;

namespace FMMI_Service.Services.APIService.BackgroundWorker
{
    public class MQTTBackgroundService : BackgroundService
    {

        private readonly IMongoDatabase _dbConnection;
        static readonly MqttTopicTemplate sampleTemplate = new("home/temp");

        private readonly IMongoCollection<TelemetriData> _telemetryCollection;

        public MQTTBackgroundService(IMongoDatabase database)
        {
            _dbConnection = database;
            _telemetryCollection = _dbConnection.GetCollection<TelemetriData>("Telemetri");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var mqttFactory = new MqttClientFactory();

            var mqttClient = mqttFactory.CreateMqttClient();
            
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("e4246b2a38b04b68b24379fef2627405.s1.eu.hivemq.cloud")
                .WithTlsOptions(_ => _.UseTls())
                .WithCredentials("MQTTC#", "Ksk199552658241")
                .Build();

            mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                try
                {
                    var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    Console.WriteLine($"Received message on topic '{e.ApplicationMessage.Topic}': {payload}");

                    // Antager at TelemetriData er defineret i FMMI_Domain.Entities
                    TelemetriData? telemetry = JsonSerializer.Deserialize<TelemetriData>(payload);

                    if (telemetry != null)
                    {
                        // Brug den pre-loadede collection til at gemme data
                        await _telemetryCollection.InsertOneAsync(telemetry, stoppingToken);
                        Console.WriteLine($"Successfully saved TelemetryData to MongoDB. ID: {telemetry.Id}");
                    }
                    else
                    {
                        Console.WriteLine("Warning: Failed to deserialize MQTT message payload.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing MQTT message: {ex.Message}");
                }
            };

            await mqttClient.ConnectAsync(mqttClientOptions, stoppingToken);

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder().WithTopicTemplate(sampleTemplate).Build();
            await mqttClient.SubscribeAsync("home/temp", MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce);
            //await mqttClient.SubscribeAsync(mqttSubscribeOptions, stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
