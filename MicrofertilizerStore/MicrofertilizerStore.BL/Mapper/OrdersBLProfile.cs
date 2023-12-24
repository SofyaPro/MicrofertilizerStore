using AutoMapper;
using MicrofertilizerStore.BL.Orders.Entities;
using MicrofertilizerStore.DataAccess.Entities;

namespace MicrofertilizerStore.BL.Mapper
{
    public class OrdersBLProfile : Profile
    {
        public OrdersBLProfile() 
        {
            CreateMap<OrderEntity, OrderModel>()
           .ForMember(x => x.Id, y => y.MapFrom(src => src.ExternalId))
           .ForMember(x => x.Status, y => y.MapFrom(src => src.Status));

            CreateMap<CreateOrderModel, OrderEntity>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.ExternalId, y => y.Ignore())
                .ForMember(x => x.ModificationTime, y => y.Ignore())
                .ForMember(x => x.CreationTime, y => y.Ignore());
        }
    }
}
