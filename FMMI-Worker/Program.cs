using FMMI_Service.Services.APIService.BackgroundWorker;
using FMMI_Worker;
using MongoDB.Driver;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();
var configuration = builder.Configuration;

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = configuration["MongoDB:ConnectionString"];
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var databaseName = configuration["MongoDB:DatabaseName"];
    return client.GetDatabase(databaseName);
});

builder.Services.AddHostedService<MQTTBackgroundService>();

var host = builder.Build();
host.Run();
