using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Api.Entities
{
    public class HotelStatus : ISoftDeletable
    {
        public const int Unconfirmed = 1;
        public const int Active = 2;
        public const int Inactive = 3;
        public const int Rejected = 4;
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
        public bool IsDeleted { get; set; }

        private class HotelStatusBuilder : IEntityTypeConfiguration<HotelStatus>
        {
            public void Configure(EntityTypeBuilder<HotelStatus> builder)
            {
                builder.ToTable(nameof(HotelStatus));
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).IsRequired();
                builder.Property(x => x.Name).IsRequired();
                builder.HasData(new HotelStatus
                {
                    Id = Unconfirmed,
                    Name = nameof(Unconfirmed)
                },
                new HotelStatus
                {
                    Id = Active,
                    Name = nameof(Active)
                },
                new HotelStatus
                {
                    Id = Inactive,
                    Name = nameof(Inactive)
                },
                new HotelStatus
                {
                    Id = Rejected,
                    Name = nameof(Rejected)
                });
                builder.HasQueryFilter(p => !p.IsDeleted);
            }
        }
    }
}