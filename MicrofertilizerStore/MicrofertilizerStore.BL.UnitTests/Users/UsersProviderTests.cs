using MicrofertilizerStore.BL.Orders;
using MicrofertilizerStore.BL.UnitTests.Mapper;
using MicrofertilizerStore.DataAccess.Entities;
using MicrofertilizerStore.DataAccess;
using Moq;
using System.Linq.Expressions;
using MicrofertilizerStore.BL.Users;

namespace MicrofertilizerStore.BL.UnitTests.Users
{
    public class UsersProviderTests
    {
        [Test]
        public void TestGetAllUsers()
        {
            Expression expression = null;
            Mock<IRepository<UserEntity>> usersRepository = new Mock<IRepository<UserEntity>>();
            usersRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Callback((Expression<Func<UserEntity, bool>> x) => { expression = x; });
            var usersProvider = new UsersProvider(usersRepository.Object, MapperHelper.Mapper);
            var users = usersProvider.GetUsers();

            usersRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()), Times.Exactly(1));
        }
        [Test]
        public void TestGetUserInfo()
        {
            Guid id = new Guid();
            Mock<IRepository<UserEntity>> usersRepository = new Mock<IRepository<UserEntity>>();
            usersRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Callback((Guid x) => { id = x; });
            var usersProvider = new UsersProvider(usersRepository.Object, MapperHelper.Mapper);
            var user = usersProvider.GetUserInfo;

            usersRepository.Verify(x => x.GetById(id), Times.Exactly(1));
        }
    }
}
