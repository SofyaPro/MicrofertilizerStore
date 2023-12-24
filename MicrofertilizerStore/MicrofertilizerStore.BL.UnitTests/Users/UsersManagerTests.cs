using MicrofertilizerStore.BL.UnitTests.Mapper;
using MicrofertilizerStore.DataAccess.Entities;
using MicrofertilizerStore.DataAccess;
using Moq;
using MicrofertilizerStore.BL.Users;
using MicrofertilizerStore.BL.Users.Entities;

namespace MicrofertilizerStore.BL.UnitTests.Users
{
    public class UsersManagerTests
    {
        [Test]
        public void TestDeleteUser()
        {
            UserEntity user = new UserEntity();

            Mock<IRepository<UserEntity>> usersRepository = new Mock<IRepository<UserEntity>>();
            usersRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(user);
            var usersManager = new UsersManager(usersRepository.Object, MapperHelper.Mapper);
            usersManager.DeleteUser(user.ExternalId);

            usersRepository.Verify(x => x.Delete(user), Times.Exactly(1));
        }

        [Test]
        public void TestUpdateUser()
        {
            UserEntity user = new UserEntity();
            Mock<IRepository<UserEntity>> usersRepository = new Mock<IRepository<UserEntity>>();
            usersRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(user);
            var usersManager = new UsersManager(usersRepository.Object, MapperHelper.Mapper);
            user.Address = "newAddress";
            var updatedUser = usersManager.UpdateUser(user.ExternalId,
                                new CreateUserModel()
                                {
                                    Id = user.ExternalId,
                                    Name = user.Name,
                                    Email = user.Email,
                                    UserType = user.UserType,
                                    PasswordHash = user.PasswordHash
                                });

            usersRepository.Verify(x => x.GetById(user.ExternalId), Times.Exactly(1));
            usersRepository.Verify(x => x.Save(user), Times.Exactly(1));
        }

        [Test]
        public void TestCreateUser()
        {
            UserEntity user = new UserEntity()
            {
                ExternalId = Guid.NewGuid(),
                Name = "testName",
                Email = "test@testemail",
                UserType = 1,
                PasswordHash = "testPasswordHash"
            };
            Mock<IRepository<UserEntity>> usersRepository = new Mock<IRepository<UserEntity>>();
            usersRepository.Setup(x => x.Save(user))
                .Callback<UserEntity>(o => user = o);
            var usersManager = new UsersManager(usersRepository.Object, MapperHelper.Mapper);
            var newUser = usersManager.CreateUser(new CreateUserModel()
            {
                Id = user.ExternalId,
                Name = user.Name,
                Email = user.Email,
                UserType = user.UserType,
                PasswordHash = user.PasswordHash

            });

            usersRepository.Verify(x => x.Save(user), Times.Exactly(1));
        }
    }
}
