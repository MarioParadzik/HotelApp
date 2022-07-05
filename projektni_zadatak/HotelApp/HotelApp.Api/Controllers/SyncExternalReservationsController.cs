using HotelApp.Api.Entities;
using HotelApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = Role.SuperAdministrator)]
    [Route("api/reservations/external")]
    public class SyncExternalReservationsController : Controller
    {
        private readonly ILogger _logger;
        private readonly ISyncReservationRepository _syncRepo;


        public SyncExternalReservationsController(ILogger<AdministratorController> logger, ISyncReservationRepository syncRepo)
        {
            _logger = logger;
            _syncRepo = syncRepo;
        }

        [HttpGet]
        public async Task<IActionResult> SyncExternalApi()
        {
            _logger.LogInformation("Syncing all reservations from external api.");
            var result = await _syncRepo.SyncExternalReservations();
            _logger.LogInformation("Returned {count} reservations from external api.", result.Count());
            return Ok(result);
        }
    }
}