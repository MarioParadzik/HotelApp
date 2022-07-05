using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Api.Controllers
{
    [Route("api/hotel")]
    [ApiController]
    public class HotelController : Controller
    {
        private readonly ILogger _logger;
        private readonly IHotelRepository _hotelRepo;

        public HotelController(ILogger<HotelController> logger, IHotelRepository hotelRepo)
        {
            _logger = logger;
            _hotelRepo = hotelRepo;
        }

        [Authorize(Roles = Role.HotelManager + "," + Role.RegisteredUser)]
        [HttpGet]
        public IActionResult GetHotels()
        {
            _logger.LogInformation("Listing all hotels.");
            var hotels = _hotelRepo.GetHotels();
            return Ok(hotels);
        }

        [Authorize(Roles = Role.HotelManager)]
        [HttpGet("my-hotels")]
        public IActionResult GetHotelManagersHotels()
        {
            _logger.LogInformation("Listing all managers hotels.");
            var hotels = _hotelRepo.GetHotelManagersHotels();
            return Ok(hotels);
        }

        [Authorize(Roles = Role.HotelManager + "," + Role.RegisteredUser)]
        [HttpGet("{id}")]
        public IActionResult GetHotelsById(int id)
        {
            _logger.LogInformation("Listing hotel with id: {id}.", id);
            var hotel = _hotelRepo.GetHotelById(id);
            return Ok(hotel);
        }

        [Authorize(Roles = Role.HotelManager + "," + Role.RegisteredUser)]
        [HttpPost]
        public IActionResult RegisterHotel([FromBody] HotelRegistrationDto hotel)
        {
            _logger.LogInformation("Adding new hotel.");
            var registration = _hotelRepo.RegisterHotelAsync(hotel);
            Response.StatusCode = StatusCodes.Status201Created;
            return new JsonResult(new EntityCreatedDto
            {
                Id = registration.Result.Id,
                Name = registration.Result.Name,
                Message = "Hotel successfully registered!"
            });
        }

        [Authorize(Roles = Role.HotelManager)]
        [HttpPut("{id}")]
        public IActionResult UpdateHotel([FromBody] HotelUpdateDto hotel, int id)
        {
            _logger.LogInformation("Updating hotel with id: {id}.", id);
            var update = _hotelRepo.UpdateHotel(hotel, id);
            Response.StatusCode = StatusCodes.Status200OK;
            return new JsonResult(new EntityCreatedDto
            {
                Id = update.Id,
                Name = update.Name,
                Message = "Hotel successfully updated!"
            });
        }

        [Authorize(Roles = Role.SuperAdministrator + "," + Role.Administrator)]
        [HttpGet("unconfirmedHotels")]
        public IActionResult GetUnconfirmedHotels()
        {
            _logger.LogInformation("Listing all unconfirmed hotels.");
            var hotels = _hotelRepo.GetUnconfirmedHotels();
            return Ok(hotels);
        }

        [Authorize(Roles = Role.HotelManager)]
        [HttpGet("{id}/rooms")]
        public IActionResult GetRoomsByHotelId(int id, [FromQuery] RoomsResourceParameters roomsResourceParameters)
        {
            var result = _hotelRepo.GetRoomsByHotelId(roomsResourceParameters, id);
            return Ok(result);
        }

        [Authorize(Roles = Role.SuperAdministrator + "," + Role.Administrator)]
        [HttpPatch("{id}/status")]
        public IActionResult UpdateHotelStatus(int id, [FromBody] StatusIdWrapper statusId)
        {
            var result = _hotelRepo.UpdateHotelStatus(id, statusId.StatusId);
            return new JsonResult(new EntityCreatedDto
            {
                Id = result.Id,
                Name = result.Name,
                Message = "Hotel status successfully updated!"
            });
        }
    }
}
