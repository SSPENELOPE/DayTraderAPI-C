using DayTraderProAPI.Application.CustomService;
using DayTraderProAPI.Core.Interfaces;
using DayTraderProAPI.Dtos;
using DayTraderProAPI.Infastructure.Data;
using DayTraderProAPI.Infastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace DayTraderProAPI
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConn = configuration.GetConnectionString("DbConn");

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(dbConn));

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(dbConn));

        }

        public static void AddIdentityAndControllers(this IServiceCollection services, IConfiguration configuration)
        {
            var apiKey = configuration.GetSection("CBKey").Value;
            var secretKey = configuration.GetSection("CBSecret").Value;

            services.AddScoped<IOrderService>(provider =>
            {
                return new OrderService(apiKey, secretKey);
            });

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IMarketSubscription>(provider =>
            {
                return new MarketDataSubscription(apiKey, secretKey);
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                });
            });

        }

        public static void AddConfigurationKeys(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CBApiKeyDto>(configuration.GetSection("CBKey"));
        }
    }
}
