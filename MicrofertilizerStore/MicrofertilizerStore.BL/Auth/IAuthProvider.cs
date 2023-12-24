using MicrofertilizerStore.BL.Auth.Entities;

namespace MicrofertilizerStore.BL.Auth
{
    public interface IAuthProvider
    {
        Task<TokensResponse> AuthorizeUser(string email, string password);
        Task RegisterUser(string email, string password);
    }
}
