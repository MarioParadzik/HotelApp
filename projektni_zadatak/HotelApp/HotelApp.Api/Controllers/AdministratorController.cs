using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Exceptions;
using HotelApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = Role.SuperAdministrator)]
    [Route("api/administrator")]
    public class AdministratorController : Controller
    {
        private readonly ILogger _logger;
        private readonly IAdministratorRepository _administratorRepo;
        private readonly UserManager<User> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AdministratorController(ILogger<AdministratorController> logger, ITokenRepository tokenRepository, UserManager<User> userManager, IAdministratorRepository administratorRepository)
        {
            _logger = logger;
            _administratorRepo = administratorRepository;
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdministrators()
        {
            _logger.LogInformation("Listing all administrators.");
            var administrators = await _administratorRepo.GetAdministrators();
            if (administrators == null) return BadRequest();
            _logger.LogInformation("Returned {count} administrators from database.", administrators.Count());
            return Ok(administrators);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdministrator([FromBody] CreateAdministratorDto registerAdministrator)
        {
            _logger.LogInformation("Registrating administrator.");
            if (registerAdministrator == null) return BadRequest();
            await _administratorRepo.CreateAdministratorAsync(registerAdministrator);
            var user = await _userManager.FindByEmailAsync(registerAdministrator.Email);
            var token = await _tokenRepository.GenerateToken(user.Email);

            Response.StatusCode = StatusCodes.Status201Created;
            _logger.LogInformation("User {UserEmail} registered.", user.Email);

            return Ok(new ResponseDto
            {
                Name = user.FirstName,
                Token = token,
                Role = _userManager.GetRolesAsync(user).Result[0]

            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveAdministrator(int id)
        {
            _logger.LogInformation("Removed administrator with id: {id}", id);
            return Ok(await _administratorRepo.RemoveAdministratorAsync(id));
        }
    }
}
