namespace MicrofertilizerStore.Service.Settings
{
    public static class MicrofertilizerStoreSettingsReader
    {
        public static MicrofertilizerStoreSettings Read(IConfiguration configuration)
        {
            return new MicrofertilizerStoreSettings()
            {
                MicrofertilizerStoreDbContextConnectionString = configuration.GetValue<string>("MicrofertilizerStoreDbContext")
            };
        }
    }
}
