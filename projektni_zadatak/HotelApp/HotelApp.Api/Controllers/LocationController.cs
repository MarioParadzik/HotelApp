using HotelApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Api.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationController : Controller
    {
        private readonly ILocationRepository _locationRepo;

        public LocationController(ILocationRepository locationRepo)
        {
            _locationRepo = locationRepo;
        }

        [HttpGet]
        public IActionResult GetLocations()
        {
            var locations = _locationRepo.GetLocations();
            return Ok(locations);
        }
        [HttpGet("{id}")]
        public IActionResult GetLocationById(int id)
        {
            var location = _locationRepo.GetLocationById(id);
            return Ok(location);
        }
    }
}
