using FMMI_Domain;
using FMMI_Service.Services.APIService.BackgroundWorker;
using FMMI_Service.Services.TelemetriService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "FMMI-API", Version = "v1" });
});

builder.Services.AddDbContext<FMMIContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Validate that the token is signed with the specified key
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),

            // Disable issuer and audience validation for testing purposes
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Register IMongoClient (the correct type for dependency injection)
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    // Hent forbindelsesstrengen fra konfigurationen
    var connectionString = configuration["MongoDB:ConnectionString"];
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    // Få den registrerede IMongoClient
    var client = sp.GetRequiredService<IMongoClient>();

    // Hent databasenavnet
    var databaseName = configuration["MongoDB:DatabaseName"];

    // Returner databasen
    return client.GetDatabase(databaseName);
});

builder.Services.AddHostedService<MQTTBackgroundService>()
.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
