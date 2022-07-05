namespace HotelApp.Api.DTO
{
    public class ExternalApiDto
    {
        public string ReservationNumber { get; set; }
        public int RoomId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int StatusId { get; set; }
    }
}
