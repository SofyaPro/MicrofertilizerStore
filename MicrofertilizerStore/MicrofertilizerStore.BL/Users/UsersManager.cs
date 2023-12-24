using AutoMapper;
using MicrofertilizerStore.DataAccess.Entities;
using MicrofertilizerStore.DataAccess;
using MicrofertilizerStore.BL.Users.Entities;

namespace MicrofertilizerStore.BL.Users
{
    public class UsersManager : IUsersManager
    {
        private readonly IRepository<UserEntity> _usersRepository;
        private readonly IMapper _mapper;

        public UsersManager(IRepository<UserEntity> usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public UserModel CreateUser(CreateUserModel model)
        {
            if (model.Email == "")
            {
                throw new ArgumentException("Email must be specified.");
            }

            if (model.Name == "")
            {
                throw new ArgumentException("Name must be specified");
            }
            if (model.PhoneNumber == "")
            {
                throw new ArgumentException("Phone number must be valid.");
            }
            if (model.PasswordHash == "")
            {
                throw new ArgumentException("Password must be valid.");
            }
            if (model.UserType < 1 || model.UserType > 2)
            {
                throw new ArgumentException("The user type doesn't exist. Only individuals or companies can be users.");
            }
            if (model.UserType == 2)
            {
                if (model.CEO is null)
                {
                    throw new ArgumentException("User type is company. CEO must be specified.");
                }
                if (model.INN is null)
                {
                    throw new ArgumentException("User type is company. INN must be specified.");
                }
            }
            var user = _mapper.Map<UserEntity>(model);

            _usersRepository.Save(user); //id, modification, externalId

            return _mapper.Map<UserModel>(user);
        }

        public void DeleteUser(Guid userId)
        {
            var user = _usersRepository.GetById(userId);
            if (user is null)
            {
                throw new ArgumentException("User not found.");
            }

            _usersRepository.Delete(user);
        }

        public UserModel UpdateUser(Guid userId, CreateUserModel model)
        {
            //validate data
            var user = _usersRepository.GetById(userId);
            if (user is null)
            {
                throw new ArgumentException("User not found.");
            }
            if (model.Email == "")
            {
                throw new ArgumentException("Email must be specified.");
            }

            if (model.Name == "")
            {
                throw new ArgumentException("Name must be specified");
            }
            if (model.PhoneNumber == "")
            {
                throw new ArgumentException("Phone number must be valid.");
            }
            if (model.PasswordHash == "")
            {
                throw new ArgumentException("Password must be valid.");
            }
            if (model.UserType < 1 || model.UserType > 2)
            {
                throw new ArgumentException("The user type doesn't exist. Only individuals or companies can be users.");
            }
            if (model.UserType == 2)
            {
                if (model.CEO is null)
                {
                    throw new ArgumentException("User type is company. CEO must be specified.");
                }
                if (model.INN is null)
                {
                    throw new ArgumentException("User type is company. INN must be specified.");
                }
            }
            user.Email = model.Email;
            user.Address = model.Address;
            user.PasswordHash = model.PasswordHash;
            user.CreditCardNumber = model.CreditCardNumber;
            user.UserType = model.UserType;
            user.Name = model.Name;
            user.PhoneNumber = model.PhoneNumber;
            user.CEO = model.CEO;
            user.INN = model.INN;

            _usersRepository.Save(user);
            return _mapper.Map<UserModel>(user);
        }
    }
}
