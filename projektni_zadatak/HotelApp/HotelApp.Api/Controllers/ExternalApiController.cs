using HotelApp.Api.DTO;
using HotelApp.Api.Filter;
using HotelApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Api.Controllers
{
    [ApiController]
    [Route("api/reservations/external-integration")]
    public class ExternalApiController : Controller
    {
        private readonly ILogger _logger;
        private readonly IExternalApiClient _externalClient;
        public ExternalApiController(ILogger<ExternalApiController> logger,  IExternalApiClient client)
        {
            _logger = logger;
            _externalClient = client;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservations([FromQuery] ExternalApiResourceParameters externalApiFilters)
        {
            _logger.LogInformation("Listing all reservations from external api.");
            var result = await _externalClient.GetReservations();
            _logger.LogInformation("Returned {count} reservations from database.", result.Count());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            _logger.LogInformation("Listing reservation with id: {id} from external api.", id);
            var result = await _externalClient.GetReservationById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddReservationToApi([FromBody] ExternalApiDto externalDto)
        {
            _logger.LogInformation("Adding new reservation to external api.");
            var result = await _externalClient.AddReservation(externalDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservationFromApi([FromBody] ExternalApiDto externalDto, int id)
        {
            _logger.LogInformation("Updating external reservation with id: {id}.", id);
            var result = await _externalClient.UpdateReservation(externalDto, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservationFromApi(int id)
        {
            _logger.LogInformation($"Deleting external reservation with id: {id}.");
            var result = await _externalClient.DeleteReservation(id);
            return Ok(result);
        }
    }
}
