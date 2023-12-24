using MicrofertilizerStore.Service.Settings;

namespace MicrofertilizerStore.Service.UnitTests.Helpers
{
    public static class TestSettingsHelper
    {
        public static MicrofertilizerStoreSettings GetSettings()
        {
            return MicrofertilizerStoreSettingsReader.Read(ConfigurationHelper.GetConfiguration());
        }
    }
}
