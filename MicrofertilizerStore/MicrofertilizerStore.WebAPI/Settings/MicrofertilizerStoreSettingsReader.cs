namespace MicrofertilizerStore.WebAPI.Settings
{
    public static class MicrofertilizerStoreSettingsReader
    {
        public static MicrofertilizerStoreSettings Read(IConfiguration configuration)
        {
            //чтение настроек приложения из конфига
            return new MicrofertilizerStoreSettings();
        }
    }
}
