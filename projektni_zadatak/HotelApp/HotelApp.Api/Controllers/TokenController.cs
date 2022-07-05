using HotelApp.Api.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using HotelApp.Api.DTO;
using Microsoft.AspNetCore.Authorization;
using HotelApp.Api.Entities;
using HotelApp.Api.Services;
using HotelApp.Api.Exceptions;

namespace HotelApp.Api.Controllers
{
    [Route("api/authentification")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepo;

        public TokenController(ILogger<TokenController> logger, UserManager<User> userManager, ITokenRepository tokenRepository, IUserRepository userRepo)
        {
            _logger = logger;
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _userRepo = userRepo;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LogIn([FromBody] UserLogInDto logingUser, CancellationToken ctoken)
        {
            if (logingUser == null) return BadRequest();

            if (await IsValidUsernameAndPassowrd(logingUser.Email, logingUser.Password))
            {
                _logger.LogInformation($"Logged in {logingUser.Email}");
                var user = await _userManager.FindByEmailAsync(logingUser.Email);
                var token = await _tokenRepository.GenerateToken(logingUser.Email);

                return Ok(new ResponseDto
                {
                    Name = user.FirstName,
                    Token = token,
                    Role = _userManager.GetRolesAsync(user).Result[0]

                });
            }
            else
            {
                throw new BadRequestException("Invalid username or password.");
            }
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegistrationDto registerUser)
        {
            if (registerUser == null) return BadRequest();
            var result = await _userRepo.RegisterUserAsync(registerUser);
            if (!result.Succeeded) throw new BadRequestException("Something went wrong while registration.");
            var user = await _userManager.FindByEmailAsync(registerUser.Email);
            var token = await _tokenRepository.GenerateToken(registerUser.Email);

            Response.StatusCode = StatusCodes.Status201Created;
            _logger.LogInformation("User {UserEmail} registered.", registerUser.Email);
            return new JsonResult(new ResponseDto
            {
                Name = user.FirstName,
                Token = token,
                Role = _userManager.GetRolesAsync(user).Result[0]

            });
        }


        private async Task<bool> IsValidUsernameAndPassowrd(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            return await _userManager.CheckPasswordAsync(user, password);
        }

    }
}
