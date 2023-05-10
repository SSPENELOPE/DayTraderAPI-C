using DayTraderProAPI.Infastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace DayTraderProAPI
{
    public static class WebApplicationExtensions
    {
        public static void MigrateIdentityContext(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetService<ILoggerFactory>();

            try
            {
                var identityContext = services.GetRequiredService<IdentityContext>();
                identityContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<IdentityContext>();
                logger.LogError(ex, "Something Went Wrong During Migration");
            }
        }
    }
}
