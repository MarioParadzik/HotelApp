using HotelApp.Api.DTO;
using HotelApp.Api.Filter;

namespace HotelApp.Api.Services
{
    public interface IExternalApiClient
    {
        public Task<List<ExternalApiDto>> GetReservations();
        public Task<ExternalApiDto> GetReservationById(int id);
        public Task<string> AddReservation(ExternalApiDto externalApiFilters);
        public Task<string> UpdateReservation(ExternalApiDto externalApiFilters, int id);
        public Task<string> DeleteReservation(int id);
    }
}
