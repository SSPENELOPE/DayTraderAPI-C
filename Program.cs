using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DayTraderProAPI.Core.Entities.Identity;
using DayTraderProAPI.Infastructure.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddUserSecrets<Program>().Build();

// Add services to the container.

builder.Services.AddDbContext<IdentityContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
    });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = scope.ServiceProvider.GetService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<IdentityContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var loger = loggerFactory.CreateLogger<IdentityContext>();
        loger.LogError(ex, "Something Went Wrong During Migration");
    }
}

// Configure the HTTP request pipeline.

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
