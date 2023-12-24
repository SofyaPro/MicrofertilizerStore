using AutoMapper;
using MicrofertilizerStore.BL.Users.Entities;
using MicrofertilizerStore.Service.Controllers.Entities;

namespace MicrofertilizerStore.Service.Mapper
{
    public class UsersServiceProfile : Profile
    {
        public UsersServiceProfile() 
        {
            CreateMap<UsersFilter, UsersModelFilter>();
            CreateMap<CreateUserRequest, CreateUserModel>();
        }
    }
}
