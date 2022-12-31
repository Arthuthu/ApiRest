using ApiRest.Dto;
using ApiRest.Models;
using ApiRest.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static UserModel user = new UserModel();
        private readonly IConfiguration _configuration;
        private readonly IPasswordProcessors _passwordProcessors;
        private readonly IUserToken _userToken;

        public AuthController(IConfiguration configuration,
            IPasswordProcessors passwordProcessors,
            IUserToken userToken)
        {
            _configuration = configuration;
            _passwordProcessors = passwordProcessors;
            _userToken = userToken;
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
    }
}
