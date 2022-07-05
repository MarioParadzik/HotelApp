namespace HotelApp.Api.Filter
{
    public class ReservationResourceParameters : PaginationFilter
    {
        public ReservationResourceParameters() { OrderBy = "DateCreated"; }
        public int[]? ReservationStatusIds { get; set; }
        public int[]? HotelIds { get; set; }
    }
}
