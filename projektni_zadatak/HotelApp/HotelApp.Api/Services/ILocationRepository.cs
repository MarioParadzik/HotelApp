using HotelApp.Api.Entities;

namespace HotelApp.Api.Services
{
    public interface ILocationRepository
    {
        public Task<bool> LocationExists(int locationId, CancellationToken token = default);
        public IEnumerable<Location> GetLocations();
        public Location GetLocationById(int id);
    }
}
