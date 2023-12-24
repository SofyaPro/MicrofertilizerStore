using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace MicrofertilizerStore.Service.UnitTests.Helpers
{
    public static class ConfigurationHelper
    { 
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }
    }
}
