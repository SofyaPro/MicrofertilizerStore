using MicrofertilizerStore.DataAccess.Entities;
using MicrofertilizerStore.DataAccess;
using NUnit.Framework;
using FluentAssertions;

namespace MicrofertilizerStore.UnitTests.Repository
{
    [TestFixture]
    [Category("Integration")]
    public class OrderRepositoryTests : RepositoryTestsBaseClass
    {
        [Test]
        public void GetAllOrdersTest()
        {
            // prepare
            using var context = DbContextFactory.CreateDbContext();
            var orders = new OrderEntity[]
            {
            new OrderEntity()
            {
                Status = "delivered",
                AmountPayable = 1000.00M,
                Paid = true,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "TestAddress1",
                Returned = false,
                PaymentMethod = true,
                BuyerName = "TestName",
                PhoneNumber = "1234567890",
                ExternalId = Guid.NewGuid()
            },
            new OrderEntity()
            {
                Status = "in transit",
                AmountPayable = 2000.00M,
                Paid = false,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "TestAddress2",
                Returned = false,
                PaymentMethod = false,
                BuyerName = "TestName",
                PhoneNumber = "1234567899",
                ExternalId = Guid.NewGuid()
            }
            };
            context.Orders.AddRange(orders);
            context.SaveChanges();

            //execute
            var repository = new Repository<OrderEntity>(DbContextFactory);
            var actualOrders = repository.GetAll();

            //assert        
            actualOrders.Should().BeEquivalentTo(orders, options => options.Excluding(x => x.User));
        }

        [Test]
        public void GetAllOrdersWithFilterTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();
            var orders = new OrderEntity[]
            {
            new OrderEntity()
            {
                Status = "delivered",
                AmountPayable = 1000.00M,
                Paid = true,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "TestAddress1",
                Returned = false,
                PaymentMethod = true,
                BuyerName = "TestName",
                PhoneNumber = "1234567890",
                ExternalId = Guid.NewGuid()
            },
            new OrderEntity()
            {
                Status = "in transit",
                AmountPayable = 2000.00M,
                Paid = false,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "TestAddress2",
                Returned = false,
                PaymentMethod = false,
                BuyerName = "TestName",
                PhoneNumber = "1234567899",
                ExternalId = Guid.NewGuid()
            }
            };
            context.Orders.AddRange(orders);
            context.SaveChanges();

            //execute
            var repository = new Repository<OrderEntity>(DbContextFactory);
            var actualOrders = repository.GetAll(x => x.DeliveryAddress == "TestAddress1").ToArray();

            //assert
            actualOrders.Should().BeEquivalentTo(orders.Where(x => x.DeliveryAddress == "TestAddress1"));
        }

        [Test]
        public void SaveNewOrdersTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();
            
            //execute
            var order = new OrderEntity()
            {
                Status = "delivered",
                AmountPayable = 1000.00M,
                Paid = true,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "TestAddress1",
                PaymentMethod = true,
                BuyerName = "TestName",
                PhoneNumber = "1234567890",
                ExternalId = Guid.NewGuid()
            };
            var repository = new Repository<OrderEntity>(DbContextFactory);
            repository.Save(order);

            //assert
            var actualOrder = context.Orders.SingleOrDefault();
            actualOrder.Should().BeEquivalentTo(order, options => options.Excluding(x => x.Id)
                .Excluding(x => x.ModificationTime)
                .Excluding(x => x.CreationTime)
                .Excluding(x => x.ExternalId));
            actualOrder.Id.Should().NotBe(default);
            actualOrder.ModificationTime.Should().NotBe(default);
            actualOrder.CreationTime.Should().NotBe(default);
            actualOrder.ExternalId.Should().NotBe(Guid.Empty);
        }

        [Test]
        public void UpdateOrderTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();

            //execute
            var order = new OrderEntity()
            {
                Status = "in transit",
                AmountPayable = 1000.00M,
                Paid = false,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "TestAddress1",
                Returned = false,
                PaymentMethod = true,
                BuyerName = "TestName",
                PhoneNumber = "1234567890",
                ExternalId = Guid.NewGuid()
            };
            order.Status = "delivered";
            order.Paid = true;
            order.Returned = true;
            var repository = new Repository<OrderEntity>(DbContextFactory);
            repository.Save(order);

            //assert
            var actualOrder = context.Orders.SingleOrDefault();
            actualOrder.Should().BeEquivalentTo(order);
        }

        [Test]
        public void DeleteOrderTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();
            var order = new OrderEntity()
            {
                Status = "delivered",
                AmountPayable = 1000.00M,
                Paid = true,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "TestAddress1",
                PaymentMethod = true,
                BuyerName = "TestName",
                PhoneNumber = "1234567890",
                ExternalId = Guid.NewGuid()
            };
            context.Orders.Add(order);
            context.SaveChanges();

            //execute
            var repository = new Repository<OrderEntity>(DbContextFactory);
            repository.Delete(order);

            //assert
            context.Orders.Count().Should().Be(0);
        }
        [Test]
        public void GetByIdOrderTest()
        {
            //prepare
            using var context = DbContextFactory.CreateDbContext();
            var orders = new OrderEntity[]
            {
                 new OrderEntity()
                {
                    Status = "delivered",
                    AmountPayable = 1000.00M,
                    Paid = true,
                    DeliveryDate = DateTime.Now,
                    DeliveryAddress = "TestAddress1",
                    Returned = false,
                    PaymentMethod = true,
                    BuyerName = "TestName",
                    PhoneNumber = "1234567890",
                    ExternalId = Guid.NewGuid()
                },
                new OrderEntity()
                {
                    Status = "in transit",
                    AmountPayable = 2000.00M,
                    Paid = false,
                    DeliveryDate = DateTime.Now,
                    DeliveryAddress = "TestAddress2",
                    Returned = false,
                    PaymentMethod = false,
                    BuyerName = "TestName",
                    PhoneNumber = "1234567899",
                    ExternalId = Guid.NewGuid()
                }
            };
            context.Orders.AddRange(orders);
            context.SaveChanges();

            //positive case
            //execute
            var repository = new Repository<OrderEntity>(DbContextFactory);
            var actualOrder = repository.GetById(orders[0].Id);
            //assert
            actualOrder.Should().BeEquivalentTo(orders[0]);

            //negative case
            //execute
            actualOrder = repository.GetById(orders[orders.Length-1].Id+10);
            //assert
            actualOrder.Should().BeNull();
        }

        [SetUp]
        public void SetUp()
        {
            CleanUp();
        }

        [TearDown]
        public void TearDown()
        {
            CleanUp();
        }

        public void CleanUp()
        {
            using (var context = DbContextFactory.CreateDbContext())
            {
                context.Orders.RemoveRange(context.Orders);
                context.SaveChanges();
            }
        }
    }
}
