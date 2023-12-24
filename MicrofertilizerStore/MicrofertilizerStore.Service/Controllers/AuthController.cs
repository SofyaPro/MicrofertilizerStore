﻿using Microsoft.AspNetCore.Mvc;
using MicrofertilizerStore.BL.Auth;

namespace MicrofertilizerStore.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthProvider _authProvider;

        public AuthController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        [HttpGet]
        [Route("login")] //.../auth/login
        public async Task<IActionResult> LoginUser([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                var tokens = await _authProvider.AuthorizeUser(email, password);

                return Ok(tokens);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(string email, string password)
        {
            try
            {
                await _authProvider.RegisterUser(email, password);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
