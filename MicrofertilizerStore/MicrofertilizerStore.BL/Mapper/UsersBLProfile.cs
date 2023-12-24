using AutoMapper;
using MicrofertilizerStore.BL.Users.Entities;
using MicrofertilizerStore.DataAccess.Entities;

namespace MicrofertilizerStore.BL.Mapper
{
    public class UsersBLProfile : Profile
    {
        public UsersBLProfile() 
        {
            CreateMap<UserEntity, UserModel>()
           .ForMember(x => x.Id, y => y.MapFrom(src => src.ExternalId))
           .ForMember(x => x.Email, y => y.MapFrom(src => src.Email))
           .ForMember(x => x.PasswordHash, y => y.MapFrom(src => src.PasswordHash))
           .ForMember(x => x.UserType, y => y.MapFrom(src => src.UserType))
           .ForMember(x => x.PhoneNumber, y => y.MapFrom(src => src.PhoneNumber))
           .ForMember(x => x.Address, y => y.MapFrom(src => src.Address == null ? "" : src.Address))
           .ForMember(x => x.BankAccount, y => y.MapFrom(src => src.CreditCardNumber == null ? "" : src.CreditCardNumber))
           .ForMember(x => x.Name, y => y.MapFrom(src => src.UserType == 1 ? src.Name : $"{src.Name} {src.CEO} {src.INN}"));

            CreateMap<CreateUserModel, UserEntity>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.ExternalId, y => y.Ignore())
                .ForMember(x => x.ModificationTime, y => y.Ignore())
                .ForMember(x => x.CreationTime, y => y.Ignore());
        }
    }
}
