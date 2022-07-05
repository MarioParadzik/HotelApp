using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Filter;
using HotelApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelApp.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ReservationController : Controller
    {
        private readonly ILogger _logger;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(ILogger<ReservationController> logger, IReservationRepository reservationRepo)
        {
            _logger = logger;
            _reservationRepository = reservationRepo;
        }

        [Authorize(Roles = Role.RegisteredUser)]
        [HttpPost("rooms/{id}/reservations")]
        public IActionResult CreateReservation([FromBody] ReservationDto reservationInfo, int id)
        {
            _logger.LogInformation("Added new reservation from room with id: {id}.", id);
            var reservation = _reservationRepository.AddReservation(reservationInfo);

            Response.StatusCode = StatusCodes.Status201Created;

            return new JsonResult(new EntityCreatedDto
            {
                Id = reservation.Id,
                Message = "Reservation successfully created!"
            });
        }

        [Authorize(Roles = Role.RegisteredUser)]
        [HttpGet("reservations/user")]
        public IActionResult GetUserReservations()
        {
            _logger.LogInformation("Listing all reservations for current user.");
            var result = _reservationRepository.GetReservationsForCurrentUser();
            return Ok(result);
        }

        [Authorize(Roles = Role.HotelManager)]
        [HttpGet("reservations/status")]
        public IActionResult GetReservationStatuses()
        {
            _logger.LogInformation("Listing all reservations statuses.");
            var reservation = _reservationRepository.GetReservationStatuses();
            _logger.LogInformation("Returned {count} reservations from database.", reservation.Count);
            return Ok(reservation);
        }

        [Authorize(Roles = Role.HotelManager + "," + Role.RegisteredUser)]
        [HttpPatch("reservations/{id}/status")]
        public IActionResult UpdateReservationStatus([FromBody] StatusIdWrapper statusId, int id)
        {
            _logger.LogInformation("Updating reservation status with id: {id}.", id);
            var reservation = _reservationRepository.UpdateReservationStatus(statusId, id);
            Response.StatusCode = StatusCodes.Status202Accepted;
            return new JsonResult(new EntityCreatedDto
            {
                Id = reservation.Id,
                Message = "Reservation status successfully updated!"
            });

        }
        [Authorize(Roles = Role.HotelManager)]
        [HttpGet("reservations")]
        public IActionResult GetReservations([FromQuery] ReservationResourceParameters reservationResourceParameters)
        {
            _logger.LogInformation("Listing all reservations.");
            var reservations = _reservationRepository.GetReservations(reservationResourceParameters);
            _logger.LogInformation("Returned {count} reservations from database.", reservations.TotalCount);
            return Ok(reservations);
        }
    }
}
