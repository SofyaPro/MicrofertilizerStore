using AutoMapper;
using MicrofertilizerStore.Service.Mapper;

namespace MicrofertilizerStore.BL.UnitTests.Mapper
{
    public static class MapperHelper
    {
        static MapperHelper()
        {
            var config = new MapperConfiguration(x => 
            {
                x.AddProfile(typeof(UsersServiceProfile));
                x.AddProfile(typeof(OrdersServiceProfile));
            });
            Mapper = new AutoMapper.Mapper(config);
        }
        public static IMapper Mapper { get; }
    }
}