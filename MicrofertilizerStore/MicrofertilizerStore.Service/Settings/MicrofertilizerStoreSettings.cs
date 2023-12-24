namespace MicrofertilizerStore.Service.Settings
{
    public class MicrofertilizerStoreSettings
    {
        public string MicrofertilizerStoreDbContextConnectionString { get; set; }
        public Uri ServiceUri { get; set; }
        public string IdentityServerUri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
