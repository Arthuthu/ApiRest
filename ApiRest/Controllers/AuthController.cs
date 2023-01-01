using ApiRest.Dto;
using ApiRest.Models;
using ApiRest.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static UserModel user = new UserModel();
        private readonly IPasswordProcessors _passwordProcessors;
        private readonly IUserToken _userToken;
        private readonly IUserService _userService;

        public AuthController(IPasswordProcessors passwordProcessors,
            IUserToken userToken,
            IUserService userService)
        {
            _passwordProcessors = passwordProcessors;
            _userToken = userToken;
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(UserDto request)
        {
            _passwordProcessors.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login(UserDto request)
        {
            if (user.Username != request.Username)
            {
                return BadRequest("User or password is incorrect");
            }

            if (!_passwordProcessors.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("User or password is incorrect");
            }

            string token = _userToken.CreateToken(user);
            return Ok(token);
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetUser()
        {
            List<string> userData = new List<string>();

            var userName = _userService.GetUserName();
            var userRole = _userService.GetUserRole();

            userData.Add($"The user name is: {userName}");
            userData.Add($"The user role is: {userRole}");

            return Ok(userData);
        }
    }
}
