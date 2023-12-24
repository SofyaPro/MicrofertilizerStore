using MicrofertilizerStore.BL.Mapper;
using MicrofertilizerStore.Service.Mapper;

namespace MicrofertilizerStore.Service.IoC
{
    public static class MapperConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<OrdersBLProfile>();
                config.AddProfile<OrdersServiceProfile>();

                config.AddProfile<UsersBLProfile>();
                config.AddProfile<UsersServiceProfile>();
            });
        }
    }
}
