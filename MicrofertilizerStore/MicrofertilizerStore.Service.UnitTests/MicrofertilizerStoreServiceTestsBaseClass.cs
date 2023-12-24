using MicrofertilizerStore.DataAccess;
using MicrofertilizerStore.Service.UnitTests.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

namespace MicrofertilizerStore.Service.UnitTests
{
    public class MicrofertilizerStoreServiceTestsBaseClass
    {
        public MicrofertilizerStoreServiceTestsBaseClass()
        {
            var settings = TestSettingsHelper.GetSettings();

            _testServer = new TestWebApplicationFactory(services =>
            {
                services.Replace(ServiceDescriptor.Scoped(_ =>
                {
                    var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                    httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                        .Returns(TestHttpClient);
                    return httpClientFactoryMock.Object;
                }));
                services.PostConfigureAll<JwtBearerOptions>(options =>
                {
                    options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                        $"{settings.IdentityServerUri}/.well-known/openid-configuration",
                        new OpenIdConnectConfigurationRetriever(),
                        new HttpDocumentRetriever(TestHttpClient) //important
                        {
                            RequireHttps = false,
                            SendAdditionalHeaderData = true
                        });
                });
            });
        }

        public T? GetService<T>()
        {
            return _testServer.Services.GetRequiredService<T>();
        }

        private readonly WebApplicationFactory<Program> _testServer;
        protected HttpClient TestHttpClient => _testServer.CreateClient();
    }
}
