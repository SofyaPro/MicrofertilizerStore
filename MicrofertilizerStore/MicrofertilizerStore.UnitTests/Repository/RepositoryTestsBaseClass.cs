using MicrofertilizerStore.DataAccess;
using MicrofertilizerStore.Service.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicrofertilizerStore.UnitTests.Repository;

public class RepositoryTestsBaseClass
{
    public RepositoryTestsBaseClass()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Test.json", optional: true)
            .Build();

        Settings = MicrofertilizerStoreSettingsReader.Read(configuration);
        ServiceProvider = ConfigureServiceProvider();

        DbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<MicrofertilizerStoreDbContext>>();
    }

    private IServiceProvider ConfigureServiceProvider()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContextFactory<MicrofertilizerStoreDbContext>(
            options => { options.UseSqlServer(Settings.MicrofertilizerStoreDbContextConnectionString); },
            ServiceLifetime.Scoped);
        return serviceCollection.BuildServiceProvider();
    }

    protected readonly MicrofertilizerStoreSettings Settings;
    protected readonly IDbContextFactory<MicrofertilizerStoreDbContext> DbContextFactory;
    protected readonly IServiceProvider ServiceProvider;
}