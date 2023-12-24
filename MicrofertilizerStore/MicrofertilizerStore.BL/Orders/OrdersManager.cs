using AutoMapper;
using MicrofertilizerStore.BL.Orders.Entities;
using MicrofertilizerStore.DataAccess;
using MicrofertilizerStore.DataAccess.Entities;

namespace MicrofertilizerStore.BL.Orders
{
    public class OrdersManager : IOrdersManager
    {
        private readonly IRepository<OrderEntity> _ordersRepository;
        private readonly IMapper _mapper;

        public OrdersManager(IRepository<OrderEntity> ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        public OrderModel CreateOrder(CreateOrderModel model)
        {
            if (model.AmountPayable <= 0)
            {
                throw new ArgumentException("Payable amount must be valid.");
            }

            if (model.DeliveryAddress is null)
            {
                throw new ArgumentException("Delivery address must be specified");
            }

            var order = _mapper.Map<OrderEntity>(model);

            _ordersRepository.Save(order); //id, modification, externalId

            return _mapper.Map<OrderModel>(order);
        }

        public void DeleteOrder(Guid orderId)
        {
            var order = _ordersRepository.GetById(orderId);
            if (order is null)
            {
                throw new ArgumentException("Order not found.");
            }

            _ordersRepository.Delete(order);
        }

        public OrderModel UpdateOrder(Guid keeperId, CreateOrderModel model)
        {
            //validate data
            var order = _ordersRepository.GetById(keeperId);
            if (order is null)
            {
                throw new ArgumentException("Order not found.");
            }
            if (model.AmountPayable <= 0)
            {
                throw new ArgumentException("Payable amount must be valid.");
            }

            if (model.DeliveryAddress is null)
            {
                throw new ArgumentException("Delivery address must be specified");
            }
            order.Status = model.Status;
            order.AmountPayable = model.AmountPayable;
            order.Paid = model.Paid;
            order.DeliveryDate = model.DeliveryDate;
            order.DeliveryAddress = model.DeliveryAddress;
            order.Returned = model.Returned;
            order.PaymentMethod = model.PaymentMethod;

            _ordersRepository.Save(order);
            return _mapper.Map<OrderModel>(order);
        }
    }
}
