using DayTraderProAPI.Application.CustomService;
using DayTraderProAPI.Core.Interfaces;
using DayTraderProAPI.Dtos;
using DayTraderProAPI.Helpers;
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
            var dbConn = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(dbConn));

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(dbConn));

        }

        public static void AddIdentityAndControllers(this IServiceCollection services, IConfiguration configuration)
        {
            var apiKey = configuration.GetSection("CBKey").Value;
            var secretKey = configuration.GetSection("CBSecret").Value;
            var clientSecret = configuration.GetSection("ClientSecret").Value;

            services.AddScoped<IOrderService>(provider =>
            {
                return new OrderService(provider.GetService<AppDbContext>(),secretKey);
            });

            services.AddScoped<ICoinBaseSignIn>(provider =>
            {
                return new CoinBaseSignInService(provider.GetService<HttpClient>(), provider.GetService<IdentityContext>(), clientSecret);
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

            services.AddAutoMapper(typeof(MappingProfiles));

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
