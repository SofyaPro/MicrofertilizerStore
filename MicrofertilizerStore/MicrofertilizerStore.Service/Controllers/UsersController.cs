using AutoMapper;
using MicrofertilizerStore.Service.Controllers.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;
using MicrofertilizerStore.BL.Users;
using MicrofertilizerStore.BL.Users.Entities;

namespace MicrofertilizerStore.Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersProvider _usersProvider;
        private readonly IUsersManager _usersManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UsersController(IUsersProvider usersProvider, IUsersManager usersManager, IMapper mapper, ILogger logger)
        {
            _usersManager = usersManager;
            _usersProvider = usersProvider;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
        {
            return Ok();
        }

        [Authorize]
        [HttpGet] // users/
        public IActionResult GetAllUsers() 
        {
            var users = _usersProvider.GetUsers();
            return Ok(new UsersListResponse()
            {
                Users = users.ToList()
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("filter")] // users/filter?filter.UserType=1
        public IActionResult GetFilteredUsers([FromQuery] UsersFilter filter)
        {
            var users = _usersProvider.GetUsers(_mapper.Map<UsersModelFilter>(filter));
            return Ok(new UsersListResponse()
            {
                Users = users.ToList()
            });
        }

        [HttpGet]
        [Route("{id}")] // users/{id}
        public IActionResult GetUserInfo([FromRoute] Guid id)
        {
            try
            {
                var user = _usersProvider.GetUserInfo(id);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                _logger.Error(ex.ToString()); //stack trace + message
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserRequest request) //automatic validation
        {
            try
            {
                var user = _usersManager.CreateUser(_mapper.Map<CreateUserModel>(request));
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                _logger.Error(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateUserInfo([FromRoute] Guid id, UpdateUserRequest request)
        {
            //validator for request
            try
            {
                var user = _usersManager.UpdateUser(id, _mapper.Map<CreateUserModel>(request));
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                _logger.Error(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            try
            {
                _usersManager.DeleteUser(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.Error(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
