namespace HotelApp.Api.DTO
{
    public class HotelInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactMail { get; set; }
        public string ContactNumber { get; set; }
        public string Adress { get; set; }
        public string Location { get; set; }
        public string HotelStatus { get; set; }
        public int HotelStatusId { get; set; }
        public int LocationId { get; set; }

    }
}
