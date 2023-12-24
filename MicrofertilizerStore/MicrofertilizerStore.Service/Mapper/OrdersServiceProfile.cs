using AutoMapper;
using MicrofertilizerStore.BL.Orders.Entities;
using MicrofertilizerStore.Service.Controllers.Entities;

namespace MicrofertilizerStore.Service.Mapper
{
    public class OrdersServiceProfile : Profile
    {
        public OrdersServiceProfile() 
        {
            CreateMap<OrdersFilter, OrdersModelFilter>();
            CreateMap<CreateOrderRequest, CreateOrderModel>();
        }
    }
}
