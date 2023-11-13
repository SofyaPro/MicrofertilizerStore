using MicrofertilizerStore.DataAccess;
using MicrofertilizerStore.Service.Settings;
using Microsoft.EntityFrameworkCore;

namespace MicrofertilizerStore.Service.IoC
{
    public static class DbContextConfigurator
    {
        public static void ConfigureService(IServiceCollection services, MicrofertilizerStoreSettings settings)
        {
            services.AddDbContextFactory<MicrofertilizerStoreDbContext>(
                options => { options.UseSqlServer(settings.MicrofertilizerStoreDbContextConnectionString); },
                ServiceLifetime.Scoped);
        }

        public static void ConfigureApplication(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MicrofertilizerStoreDbContext>>();
            using var context = contextFactory.CreateDbContext();
            context.Database.Migrate();
        }
    }
}
