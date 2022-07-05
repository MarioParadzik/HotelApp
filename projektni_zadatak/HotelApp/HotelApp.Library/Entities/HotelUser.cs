using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Api.Entities
{
    public class HotelUser : ISoftDeletable
    {
        public int HotelId { get; set; }
        public int UserId { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual User User { get; set; }
        public bool IsDeleted { get; set; }

        private class HotelUserBuilder : IEntityTypeConfiguration<HotelUser>
        {
            public void Configure(EntityTypeBuilder<HotelUser> builder)
            {
                builder.ToTable(nameof(HotelUser));
                builder.HasKey(e => new { e.HotelId, e.UserId });
                builder.HasQueryFilter(p => !p.IsDeleted);
            }
        }
    }
}
