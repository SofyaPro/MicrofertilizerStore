using MicrofertilizerStore.BL.Orders.Entities;

namespace MicrofertilizerStore.BL.Orders
{
    public interface IOrdersManager
    {
        OrderModel CreateOrder(CreateOrderModel model);
        void DeleteOrder(Guid orderId);
        OrderModel UpdateOrder(Guid orderId, CreateOrderModel model);
    }
}
