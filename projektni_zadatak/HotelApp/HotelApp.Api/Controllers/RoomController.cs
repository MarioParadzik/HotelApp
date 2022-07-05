using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelApp.Api.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomController : Controller
    {
        private readonly ILogger _logger;
        private readonly IRoomRepository _roomRepo;
        public RoomController(ILogger<RoomController> logger, IRoomRepository roomRepo)
        {
            _logger = logger;
            _roomRepo = roomRepo;
        }

        [HttpGet("{id}")]
        public IActionResult GetRoomById(int id)
        {
            _logger.LogInformation("Listing room with id: {id}.", id);
            var room = _roomRepo.GetRoomInfoById(id);
            return Ok(room);
        }


        [Authorize(Roles = Role.HotelManager)]
        [HttpPost]
        public IActionResult AddRoom([FromBody] CreateRoomDto createRoom)
        {
            _logger.LogInformation($"Added new room.");
            var room = _roomRepo.CreateRoom(createRoom);
            Response.StatusCode = StatusCodes.Status201Created;
            return new JsonResult(new EntityCreatedDto
            {
                Id = room.Id,
                Name = room.Name,
                Message = "Room successfully created!"
            });
        }

        [Authorize(Roles = Role.HotelManager)]
        [HttpPut("{id}")]
        public IActionResult UpdateRoom([FromBody] CreateRoomDto updateRoom, int id)
        {
            _logger.LogInformation("Updated room with id: {id}.", id);
            var room = _roomRepo.UpdateRoom(updateRoom, id);
            Response.StatusCode = StatusCodes.Status200OK;
            return new JsonResult(new EntityCreatedDto
            {
                Id = room.Id,
                Name = room.Name,
                Message = "Room successfully created!"
            });
        }

        [Authorize(Roles = Role.HotelManager + "," + Role.RegisteredUser)]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetRooms([FromQuery] RoomsResourceParameters roomsResourceParameters)
        {
            _logger.LogInformation("Listing all rooms.");
            var rooms = _roomRepo.GetRooms(roomsResourceParameters);
            _logger.LogInformation("Returned {count} rooms from database.", rooms.TotalCount);
            return Ok(rooms);
        }

        [Authorize(Roles = Role.HotelManager)]
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _roomRepo.DeleteRoomById(id);
            return Ok(new EntityCreatedDto
            {
                Name = room.Name,
                Message = "Room successfully deleted!"
            });
        }

        [HttpGet("bedNumbers")]
        public IActionResult GetNumberOfBeds()
        {
            var numberOfBeds = _roomRepo.GetNumberOfBeds();
            return Ok(numberOfBeds);
        }

    }
}
