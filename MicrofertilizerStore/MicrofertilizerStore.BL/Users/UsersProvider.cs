using AutoMapper;
using MicrofertilizerStore.DataAccess.Entities;
using MicrofertilizerStore.DataAccess;
using MicrofertilizerStore.BL.Users.Entities;

namespace MicrofertilizerStore.BL.Users
{
    public class UsersProvider : IUsersProvider
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;

        public UsersProvider(IRepository<UserEntity> usersRepository, IMapper mapper)
        {
            _userRepository = usersRepository;
            _mapper = mapper;
        }

        public IEnumerable<UserModel> GetUsers(UsersModelFilter modelFilter = null)
        {
            var userType = modelFilter?.UserType;

            var users = _userRepository.GetAll(x =>
                (userType == null || x.UserType == userType));

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public UserModel GetUserInfo(Guid userId)
        {
            var user = _userRepository.GetById(userId); //return null if not exists
            if (user is null)
            {
                throw new ArgumentException("User not found.");
            }

            return _mapper.Map<UserModel>(user);
        }
    }
}
