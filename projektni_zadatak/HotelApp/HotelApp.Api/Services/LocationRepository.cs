using HotelApp.Api.DbContexts;
using HotelApp.Api.Entities;
using HotelApp.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Api.Services
{
    public class LocationRepository : ILocationRepository
    {
        private readonly HotelDbContext _context;   
        public LocationRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<bool> LocationExists(int locationId, CancellationToken token = default)
        {
            return await _context.Location.Where(x => x.Id == locationId).AnyAsync(token);
        }

        public Location GetLocationById(int id)
        {
            var location = _context.Location.Find(id);
            if (location == null) throw new RecordNotFoundException($"Record with id {id} does not exist.");
            return location;
        }

        public IEnumerable<Location> GetLocations()
        {
            return _context.Location.ToList();
        }
    }
}
