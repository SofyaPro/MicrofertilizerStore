using MicrofertilizerStore.Service.IoC;
using MicrofertilizerStore.Service.Settings;

var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", optional: false)
.Build();

var settings = MicrofertilizerStoreSettingsReader.Read(configuration);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

DbContextConfigurator.ConfigureService(builder.Services, settings);
SerilogConfigurator.ConfigureService(builder);
SwaggerConfigurator.ConfigureServices(builder.Services);

var app = builder.Build();

SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);
DbContextConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
