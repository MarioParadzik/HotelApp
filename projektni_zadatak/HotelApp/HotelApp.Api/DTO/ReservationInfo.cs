using FluentValidation;

namespace HotelApp.Api.DTO
{
    public class ReservationInfoDto
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? Note { get; set; }
        public string RoomName { get; set; }
        public string HotelName { get; set; }
        public int ReservationStatusId { get; set; }
        public string ReservationStatus { get; set; }
        public bool CanCancel { get; set; }
    }

}
