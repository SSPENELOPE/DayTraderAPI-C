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
    .AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>();
    /*.AddAzureKeyVault(new Uri(Environment.GetEnvironmentVariable("VaultUri")), new DefaultAzureCredential());*/

builder.Services.AddDbContexts(builder.Configuration);

builder.Services.AddIdentityAndControllers(builder.Configuration);

builder.Services.AddConfigurationKeys(builder.Configuration);

builder.Services.AddIdentityService(builder.Configuration);

var app = builder.Build();


app.MigrateIdentityContext();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
