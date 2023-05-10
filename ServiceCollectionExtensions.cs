using DayTraderProAPI.Infastructure.Identity;
using DayTraderProAPI.Infastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

            services.AddDbContext<WatchlistContext>(options =>
                options.UseSqlServer(dbConn));

            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(dbConn));
        }

        public static void AddIdentityAndControllers(this IServiceCollection services)
        {
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
    }
}
