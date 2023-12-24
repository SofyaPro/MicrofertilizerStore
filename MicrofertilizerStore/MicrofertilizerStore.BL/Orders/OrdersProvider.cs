using AutoMapper;
using MicrofertilizerStore.BL.Orders.Entities;
using MicrofertilizerStore.DataAccess;
using MicrofertilizerStore.DataAccess.Entities;

namespace MicrofertilizerStore.BL.Orders
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IMapper _mapper;

        public OrdersProvider(IRepository<OrderEntity> ordersRepository, IMapper mapper)
        {
            _orderRepository = ordersRepository;
            _mapper = mapper;
        }

        public IEnumerable<OrderModel> GetOrders(OrdersModelFilter modelFilter = null)
        {
            var paid = modelFilter?.Paid;
            var returned = modelFilter?.Returned;
            var status = modelFilter?.Status;
            
            var orders = _orderRepository.GetAll(x => 
                (paid == null || x.Paid == paid) &&
                (returned == null || x.Returned == returned) &&
                (status == null || x.Status == status)); 

            return _mapper.Map<IEnumerable<OrderModel>>(orders);
        }

        public OrderModel GetOrderInfo(Guid orderId)
        {
            var order = _orderRepository.GetById(orderId); //return null if not exists
            if (order is null)
            {
                throw new ArgumentException("Order not found.");
            }

            return _mapper.Map<OrderModel>(order);
        }
    }
}
