using MicrofertilizerStore.BL.Users.Entities;

namespace MicrofertilizerStore.BL.Users
{
    public interface IUsersProvider
    {
        IEnumerable<UserModel> GetUsers(UsersModelFilter modelFilter = null);
        UserModel GetUserInfo(Guid userId);
    }
}
