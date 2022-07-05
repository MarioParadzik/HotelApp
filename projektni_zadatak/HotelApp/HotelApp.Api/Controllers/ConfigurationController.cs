using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = Role.SuperAdministrator)]
    [Route("api/configurations")]
    public class ConfigurationController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConfigurationRepository _configurationRepository;

        public ConfigurationController(ILogger<ConfigurationController> logger, IConfigurationRepository configurationRepo)
        {
            _logger = logger;
            _configurationRepository = configurationRepo;
        }


        [HttpPatch("{id}")]
        public IActionResult UpdateConfiguration(int id,  [FromBody] ValueWrapperDto value)
        {
            _logger.LogInformation("Updating configuration with id: {id}.", id);
            var config = _configurationRepository.UpdateConfiguration(id, value.Value);
            return Ok(config);
        }


        [HttpGet]
        public IActionResult GetConfiguration()
        {
            _logger.LogInformation("Listing all configurations.");
            var result = _configurationRepository.GetConfigurations();
            _logger.LogInformation("Returned {resultCount} configurations from database.", result.Count());
            return Ok(result);
        }

    }
}
