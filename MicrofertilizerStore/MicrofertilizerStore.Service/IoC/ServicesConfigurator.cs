using AutoMapper;
using MicrofertilizerStore.DataAccess.Entities;
using MicrofertilizerStore.DataAccess;
using Microsoft.AspNetCore.Identity;
using MicrofertilizerStore.Service.Settings;
using MicrofertilizerStore.BL.Orders;
using MicrofertilizerStore.BL.Users;
using MicrofertilizerStore.BL.Auth;


namespace MicrofertilizerStore.Service.IoC
{
    public static class ServicesConfigurator
    {
        public static void ConfigureService(IServiceCollection services, MicrofertilizerStoreSettings settings)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            services.AddScoped<IOrdersProvider>(x =>
                new OrdersProvider(x.GetRequiredService<IRepository<OrderEntity>>(), x.GetRequiredService<IMapper>()));
            services.AddScoped<IOrdersManager, OrdersManager>();

            services.AddScoped<IUsersProvider>(x =>
                new UsersProvider(x.GetRequiredService<IRepository<UserEntity>>(), x.GetRequiredService<IMapper>()));
            services.AddScoped<IUsersManager, UsersManager>();

            services.AddScoped<IAuthProvider>(x =>
                new AuthProvider(x.GetRequiredService<SignInManager<UserEntity>>(),
                    x.GetRequiredService<UserManager<UserEntity>>(),
                    x.GetRequiredService<IHttpClientFactory>(),
                    settings.IdentityServerUri,
                    settings.ClientId,
                    settings.ClientSecret));
        }
    }
}
