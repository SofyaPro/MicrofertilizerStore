using MicrofertilizerStore.BL.Users.Entities;

namespace MicrofertilizerStore.BL.Users
{
    public interface IUsersManager
    {
        UserModel CreateUser(CreateUserModel model);
        void DeleteUser(Guid userId);
        UserModel UpdateUser(Guid userId, CreateUserModel model);
    }
}
