using MicrofertilizerStore.BL.Orders.Entities;

namespace MicrofertilizerStore.BL.Orders
{
    public interface IOrdersProvider
    {
        IEnumerable<OrderModel> GetOrders(OrdersModelFilter modelFilter = null);
        OrderModel GetOrderInfo(Guid trainerId);
    }
}
