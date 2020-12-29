using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BlogEngineApi.User.Infrastructure
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public UserController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserRequest userLogin)
        {
            var authentication = _userService.DoLogin(userLogin);
            if (authentication.Code != 200)
                return Unauthorized(authentication);

            return Ok(authentication);
        }
    }
}