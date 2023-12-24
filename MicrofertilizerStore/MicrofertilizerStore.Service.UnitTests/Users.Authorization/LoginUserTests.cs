using MicrofertilizerStore.BL.Auth.Entities;
using MicrofertilizerStore.DataAccess.Entities;
using MicrofertilizerStore.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using FluentAssertions;
using IdentityModel.Client;

namespace MicrofertilizerStore.Service.UnitTests.Users.Authorization
{
    public class LoginUserTests : MicrofertilizerStoreServiceTestsBaseClass
    {
        [Test]
        public async Task SuccessFullResult()
        {
            //prepare
            var user = new UserEntity()
            {
                Email = "test@test",
                Name = "test@test",
                UserType = 1,
                PhoneNumber = "12345678901",
                PasswordHash = "testPasswordHash"
            };
            var password = "Password1@";

            using var scope = GetService<IServiceScopeFactory>().CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
            var result = await userManager.CreateAsync(user, password);

            //execute
            var query = $"?email={user.Name}&password={password}";
            var requestUri =
                MicrofertilizerStoreApiEndpoints.AuthorizeUserEndpoint + query; // /auth/login?login=test@test&password=password
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var client = TestHttpClient;
            var response = await client.SendAsync(request);

            //assert
            //response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContentJson = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<TokensResponse>(responseContentJson);

            content.Should().NotBeNull();
            content.AccessToken.Should().NotBeNull();
            content.RefreshToken.Should().NotBeNull();

            //check that access token is valid

            var requestToGetAllOrders =
                new HttpRequestMessage(HttpMethod.Get, MicrofertilizerStoreApiEndpoints.GetAllOrdersEndpoint);

            var clientWithToken = TestHttpClient;
            client.SetBearerToken(content.AccessToken);
            var getAllUsersResponse = await client.SendAsync(requestToGetAllOrders);

            getAllUsersResponse.StatusCode.Should().Be(HttpStatusCode.OK, "Authorization succeeded.");
        }

        [Test]
        public async Task BadRequestUserNotFoundResultTest()
        {
            //prepare
            var login = "not_existing@mail.ru";
            using var scope = GetService<IServiceScopeFactory>().CreateScope();
            var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();
            var user = userRepository.GetAll().FirstOrDefault(x => x.Name.ToLower() == login.ToLower());
            if (user != null)
            {
                userRepository.Delete(user);
            }

            var password = "password";
            //execute
            var query = $"?email={login}&password={password}";
            var requestUri =
                MicrofertilizerStoreApiEndpoints.AuthorizeUserEndpoint + query; // /auth/login?login=test@test&password=password
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await TestHttpClient.SendAsync(request);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "User is not found.");
        }

        [Test]
        public async Task PasswordIsIncorrectResultTest()
        {
            //prepare
            var user = new UserEntity()
            {
                Email = "test@test",
                Name = "test@test",
                UserType = 1,
                PhoneNumber = "12345678901",
                PasswordHash = "testPasswordHash"
            };
            var password = "password";

            using var scope = GetService<IServiceScopeFactory>().CreateScope();
            var userManager = scope.ServiceProvider.GetService<UserManager<UserEntity>>();
            await userManager.CreateAsync(user, password);

            var incorrect_password = "kvhdbkvhbk";

            //execute
            var query = $"?email={user.Name}&password={incorrect_password}";
            var requestUri =
                MicrofertilizerStoreApiEndpoints.AuthorizeUserEndpoint + query; // /auth/login?login=test@test&password=kvhdbkvhbk
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var client = TestHttpClient;
            var response = await client.SendAsync(request);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Wrong password."); // with some message
        }

        [Test]
        [TestCase("", "")]
        [TestCase("qwe", "")]
        [TestCase("test@test", "")]
        [TestCase("", "password")]
        public async Task LoginOrPasswordAreInvalidResultTest(string login, string password)
        {
            //execute
            var query = $"?login={login}&password={password}";
            var requestUri =
                MicrofertilizerStoreApiEndpoints.AuthorizeUserEndpoint + query; // /auth/login?login=test@test&password=kvhdbkvhbk
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var client = TestHttpClient;
            var response = await client.SendAsync(request);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Wrong password format."); // with some message
        }
    }
}
