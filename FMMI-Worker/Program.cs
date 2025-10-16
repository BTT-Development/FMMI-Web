using FMMI_Domain;
using FMMI_Service.Services.APIService.BackgroundWorker;
using FMMI_Worker;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();
var configuration = builder.Configuration;

#region PostgreSQL
builder.Services.AddDbContext<FMMIContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
#endregion

#region MongoDB
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
#endregion

builder.Services.AddHostedService<MQTTBackgroundService>();

var host = builder.Build();
host.Run();
