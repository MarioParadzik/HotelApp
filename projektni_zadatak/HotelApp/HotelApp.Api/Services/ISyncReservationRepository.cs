using HotelApp.Api.Entities;

namespace HotelApp.Api.Services
{
    public interface ISyncReservationRepository
    {
        public Task<List<Reservation>> SyncExternalReservations();
    }
}
