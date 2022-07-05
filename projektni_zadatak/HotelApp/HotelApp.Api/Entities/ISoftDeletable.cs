namespace HotelApp.Api.Entities
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
