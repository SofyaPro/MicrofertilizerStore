﻿namespace MicrofertilizerStore.WebAPI.IoC
{
    /// <summary>
    /// Static class for Swagger Configurator
    /// </summary>
    public static class SwaggerConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void ConfigureApplication(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}