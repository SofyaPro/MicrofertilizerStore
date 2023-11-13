using MicrofertilizerStore.DataAccess;
using MicrofertilizerStore.DataAccess.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace MicrofertilizerStore.UnitTests.Repository;

[TestFixture]
[Category("Integration")]
public class UserRepositoryTests : RepositoryTestsBaseClass
{
    [Test]
    public void GetAllUsersTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var users = new UserEntity[]
        {
            new UserEntity()
            {
                Email = "testemail1@mail.ru",
                Address = "TestAddress1",
                PasswordHash = "****1",
                CreditCardNumber = "**0001",
                UserType = 1,
                Name = "User Name",
                PhoneNumber = "1234567890",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Email = "testemail2@mail.ru",
                Address = "TestAddress2",
                PasswordHash = "****2",
                CreditCardNumber = "**0002",
                UserType = 2,
                Name = "Company Name",
                PhoneNumber = "123456",
                CEO = "CEO Test Name",
                INN = "1234567890",
                ExternalId = Guid.NewGuid()
            }
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUsers = repository.GetAll();

        //assert        
        actualUsers.Should().BeEquivalentTo(users);
    }

    [Test]
    public void GetAllUsersWithFilterTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var users = new UserEntity[]
        {
             new UserEntity()
            {
                Email = "testemail1@mail.ru",
                Address = "TestAddress1",
                PasswordHash = "****1",
                CreditCardNumber = "**0001",
                UserType = 1,
                Name = "User Name",
                PhoneNumber = "1234567890",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Email = "testemail2@mail.ru",
                Address = "TestAddress2",
                PasswordHash = "****2",
                CreditCardNumber = "**0002",
                UserType = 2,
                Name = "Company Name",
                PhoneNumber = "123456",
                CEO = "CEO Test Name",
                INN = "1234567890",
                ExternalId = Guid.NewGuid()
            }
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUsers = repository.GetAll(x => x.Address == "TestAddress1").ToArray();

        //assert
        actualUsers.Should().BeEquivalentTo(users.Where(x => x.Address == "TestAddress1"));
    }

    [Test]
    public void SaveNewUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        //execute
        var user = new UserEntity()
        {
            Email = "testemail1@mail.ru",
            Address = "TestAddress1",
            PasswordHash = "****1",
            CreditCardNumber = "**0001",
            UserType = 1,
            Name = "User Name",
            PhoneNumber = "1234567890",
            ExternalId = Guid.NewGuid()
        };
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Save(user);

        //assert
        var actualUser = context.Users.SingleOrDefault();
        actualUser.Should().BeEquivalentTo(user, options => options.Excluding(x => x.Id)
            .Excluding(x => x.ModificationTime)
            .Excluding(x => x.CreationTime)
            .Excluding(x => x.ExternalId));
        actualUser.Id.Should().NotBe(default);
        actualUser.ModificationTime.Should().NotBe(default);
        actualUser.CreationTime.Should().NotBe(default);
        actualUser.ExternalId.Should().NotBe(Guid.Empty);
    }

    [Test]
    public void UpdateUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var user = new UserEntity()
        {
            Email = "testemail1@mail.ru",
            Address = "TestAddress1",
            PasswordHash = "****1",
            CreditCardNumber = "**0001",
            UserType = 1,
            Name = "User Name",
            PhoneNumber = "1234567890",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(user);
        context.SaveChanges();

        //execute
        user.Email = "new email";
        user.Address = "new address";
        user.CreditCardNumber = "**0003";
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Save(user);

        //assert
        var actualUser = context.Users.SingleOrDefault();
        actualUser.Should().BeEquivalentTo(user);
    }

    [Test]
    public void DeleteUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var user = new UserEntity()
        {
            Email = "testemail1@mail.ru",
            Address = "TestAddress1",
            PasswordHash = "****1",
            CreditCardNumber = "**0001",
            UserType = 1,
            Name = "User Name",
            PhoneNumber = "1234567890",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(user);
        context.SaveChanges();

        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Delete(user);

        //assert
        context.Users.Count().Should().Be(0);
    }
    [Test]
    public void GetByIdUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var users = new UserEntity[]
        {
             new UserEntity()
            {
                Email = "testemail1@mail.ru",
                Address = "TestAddress1",
                PasswordHash = "****1",
                CreditCardNumber = "**0001",
                UserType = 1,
                Name = "User Name",
                PhoneNumber = "1234567890",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Email = "testemail2@mail.ru",
                Address = "TestAddress2",
                PasswordHash = "****2",
                CreditCardNumber = "**0002",
                UserType = 2,
                Name = "Company Name",
                PhoneNumber = "123456",
                CEO = "CEO Test Name",
                INN = "1234567890",
                ExternalId = Guid.NewGuid()
            }
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        // positive case
        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUser = repository.GetById(users[0].Id);
        //assert
        actualUser.Should().BeEquivalentTo(users[0]);

        // negative case
        //execute
        actualUser = repository.GetById(users[users.Length - 1].Id + 10);
        //assert
        actualUser.Should().BeNull();
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
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
        }
    }
}
