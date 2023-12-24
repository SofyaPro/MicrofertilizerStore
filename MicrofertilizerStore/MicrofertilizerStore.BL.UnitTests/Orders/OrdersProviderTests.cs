using MicrofertilizerStore.BL.Orders;
using MicrofertilizerStore.BL.UnitTests.Mapper;
using MicrofertilizerStore.DataAccess;
using MicrofertilizerStore.DataAccess.Entities;
using Moq;
using System.Linq.Expressions;

namespace MicrofertilizerStore.BL.UnitTests.Orders
{
    [TestFixture]
    public class OrdersProviderTests
    {
        [Test]
        public void TestGetAllOrders()
        {
            Expression expression = null;
            Mock<IRepository<OrderEntity>> ordersRepository = new Mock<IRepository<OrderEntity>>();
            ordersRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<OrderEntity, bool>>>()))
                .Callback((Expression<Func<OrderEntity, bool>> x) => { expression = x; });
            var ordersProvider = new OrdersProvider(ordersRepository.Object, MapperHelper.Mapper);
            var orders = ordersProvider.GetOrders();

            ordersRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<OrderEntity, bool>>>()), Times.Exactly(1));
        }

        [Test]
        public void TestGetOrderInfo()
        {
            Guid id = new Guid();
            Mock<IRepository<OrderEntity>> ordersRepository = new Mock<IRepository<OrderEntity>>();
            ordersRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Callback((Guid x) => { id = x; });
            var ordersProvider = new OrdersProvider(ordersRepository.Object, MapperHelper.Mapper);
            var order = ordersProvider.GetOrderInfo(id);

            ordersRepository.Verify(x => x.GetById(id), Times.Exactly(1));
        }
    }
}
