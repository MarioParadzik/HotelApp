using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Api.Entities
{
    public class Location : ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostOfficeId { get; set; }

        public virtual PostOffice PostOffice { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
        public bool IsDeleted { get; set; }

        private class LocationBuilder : IEntityTypeConfiguration<Location>
        {
            public void Configure(EntityTypeBuilder<Location> builder)
            {
                builder.ToTable(nameof(Location));
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id);
                builder.Property(x => x.Name)
                    .IsRequired();
                builder.Property(x => x.PostOfficeId)
                    .IsRequired();
                builder.HasOne(c => c.PostOffice)
                        .WithMany(p => p.Locations)
                        .HasForeignKey(e => e.PostOfficeId);
                builder.HasQueryFilter(p => !p.IsDeleted);

            }
        }
    }

}