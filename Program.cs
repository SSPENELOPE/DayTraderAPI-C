using Azure.Identity;
using DayTraderProAPI;
using DayTraderProAPI.Extensions;
using DayTraderProAPI.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("Secrets.json")
    .AddUserSecrets<Program>()
    .AddAzureKeyVault(new Uri(Environment.GetEnvironmentVariable("VaultUri")), new DefaultAzureCredential());

builder.Services.AddDbContexts(builder.Configuration);

builder.Services.AddIdentityAndControllers(builder.Configuration);

builder.Services.AddConfigurationKeys(builder.Configuration);

builder.Services.AddIdentityService(builder.Configuration);

var app = builder.Build();


app.MigrateIdentityContext();

// Configure the HTTP request pipeline.

app.UseCors("CorsPolicy")
   .UseHttpsRedirection()
   .UseAuthorization();
app.MapControllers();
app.Run();
