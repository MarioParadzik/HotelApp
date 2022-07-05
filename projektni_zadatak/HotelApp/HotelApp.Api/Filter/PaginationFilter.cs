namespace HotelApp.Api.Filter
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; }
        public int PageSize { get; set; } = 5;
    }
}
