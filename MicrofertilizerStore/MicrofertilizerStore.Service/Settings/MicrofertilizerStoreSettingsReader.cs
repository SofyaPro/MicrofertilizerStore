namespace MicrofertilizerStore.Service.Settings
{
    public static class MicrofertilizerStoreSettingsReader
    {
        public static MicrofertilizerStoreSettings Read(IConfiguration configuration)
        {
            return new MicrofertilizerStoreSettings()
            {
                MicrofertilizerStoreDbContextConnectionString = configuration.GetValue<string>("MicrofertilizerStoreDbContext"),
                ServiceUri = configuration.GetValue<Uri>("Uri"),
                IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri"),
                ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
                ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret")
            };
        }
    }
}
