using HotelApp.Api.Filter;

namespace HotelApp.Api.DTO
{
    public class RoomsResourceParameters : PaginationFilter
    {
        public RoomsResourceParameters(){ OrderBy = "Price"; }
        public int HotelStatus { get; set; }
        public int[]? NumberOfBeds { get; set; }
        public int[]? LocationIds { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
