using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Filter;
using HotelApp.Api.Helpers;

namespace HotelApp.Api.Services
{
    public interface IReservationRepository
    {
        public Reservation AddReservation(ReservationDto reservationForCreation);
        public ICollection<ReservationInfoDto> GetReservationsForCurrentUser();
        public Reservation GetReservationById(int id);
        public ICollection<ReservationStatus> GetReservationStatuses();
        public Reservation UpdateReservationStatus(StatusIdWrapper statusId, int reservationId);
        public PagedList<ReservationInfoDto> GetReservations(ReservationResourceParameters reservationFilters);
        public bool IsFreeDate(DateTime dateFrom, DateTime dateTo, int roomId);
    }
}
