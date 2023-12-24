using MicrofertilizerStore.BL.Orders;
using MicrofertilizerStore.BL.UnitTests.Mapper;
using MicrofertilizerStore.DataAccess.Entities;
using MicrofertilizerStore.DataAccess;
using Moq;
using MicrofertilizerStore.BL.Orders.Entities;

namespace MicrofertilizerStore.BL.UnitTests.Orders
{
    public class OrdersManagerTests
    {
        [Test]
        public void TestDeleteOrder()
        {
            OrderEntity order = new OrderEntity();
            Mock<IRepository<OrderEntity>> ordersRepository = new Mock<IRepository<OrderEntity>>();
            ordersRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(order);
            var ordersManager = new OrdersManager(ordersRepository.Object, MapperHelper.Mapper);
            ordersManager.DeleteOrder(order.ExternalId);

            ordersRepository.Verify(x => x.Delete(order), Times.Exactly(1));
        }

        [Test]
        public void TestUpdateOrder()
        {
            OrderEntity order = new OrderEntity();
            Mock<IRepository<OrderEntity>> ordersRepository = new Mock<IRepository<OrderEntity>>();
            ordersRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(order);
            var ordersManager = new OrdersManager(ordersRepository.Object, MapperHelper.Mapper);
            order.Status = "Delivered";
            var updatedOrder = ordersManager.UpdateOrder(order.ExternalId, 
                                new CreateOrderModel()
                                {
                                    Id = order.ExternalId,
                                    Status = order.Status,
                                    DeliveryAddress = order.DeliveryAddress,
                                    AmountPayable = order.AmountPayable,
                                    DeliveryDate = order.DeliveryDate,
                                    PaymentMethod = order.PaymentMethod,
                                    Paid = order.Paid,
                                    Returned = order.Returned
                                });

            ordersRepository.Verify(x => x.GetById(order.ExternalId), Times.Exactly(1));
            ordersRepository.Verify(x => x.Save(order), Times.Exactly(1));
        }

        [Test]
        public void TestCreateOrder()
        {
            OrderEntity order = new OrderEntity()
            {
                ExternalId = Guid.NewGuid(),
                Status = "Registered",
                DeliveryAddress = "Test address",
                AmountPayable = 1000,
                DeliveryDate = DateTime.Now,
                PaymentMethod = true,
                Paid = true,
                Returned = true,
            };
            Mock<IRepository<OrderEntity>> ordersRepository = new Mock<IRepository<OrderEntity>>();
            ordersRepository.Setup(x => x.Save(order))
                .Callback<OrderEntity>(o => order = o);
            var ordersManager = new OrdersManager(ordersRepository.Object, MapperHelper.Mapper);
            var newOrder = ordersManager.CreateOrder(new CreateOrderModel()
            {
                Id = order.ExternalId,
                DeliveryAddress = order.DeliveryAddress,
                Status = order.Status,
                AmountPayable = order.AmountPayable,
                DeliveryDate = order.DeliveryDate,
                PaymentMethod = order.PaymentMethod,
                Paid = order.Paid,
                Returned = order.Returned
            });

            ordersRepository.Verify(x => x.Save(order), Times.Exactly(1));
        }
    }
}
