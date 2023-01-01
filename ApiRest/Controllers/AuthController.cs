using ApiRest.Dto;
using ApiRest.Models;
using ApiRest.Repositories;
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
        private readonly IUserRepository _userRepository;

        public AuthController(IPasswordProcessors passwordProcessors,
            IUserToken userToken,
            IUserService userService,
            IUserRepository userRepository)
        {
            _passwordProcessors = passwordProcessors;
            _userToken = userToken;
            _userService = userService;
            _userRepository = userRepository;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(UserDto request)
        {
            UserModel user = new();

            _passwordProcessors.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Username = request.Username;
            user.Password = request.Password;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = "Admin";

            var databaseUsers = await _userRepository.GetAllUsers();

            foreach (var databaseUser in databaseUsers)
            {
                if (databaseUser.Username == user.Username)
                {
                    return BadRequest("The username is already registered");
                }
            }

            await _userRepository.CreateUser(user);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login(UserDto request)
        {
            var user = _userRepository.GetUserByLogin(request);

            try
            {
                if (user.Username is null)
                {
                    return BadRequest("User or password is incorrect");
                }
            }
            catch (Exception)
            {
                return BadRequest("User or password is incorret");
            }


            if (!_passwordProcessors.VerifyPasswordHash(user.Password,
                user.PasswordHash,
                user.PasswordSalt))
            {
                return BadRequest("User or password is incorrect");
            }

            string token = _userToken.CreateToken(AuthController.user);
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
