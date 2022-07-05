using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Api.Entities
{
    public class Room : ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfBeds { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual Hotel Hotel { get; set; }
        public bool IsDeleted { get; set; }

        private class RoomBuilder : IEntityTypeConfiguration<Room>
        {
            public void Configure(EntityTypeBuilder<Room> builder)
            {
                builder.ToTable(nameof(Room));
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id);
                builder.Property(x => x.Name)
                    .IsRequired();
                builder.Property(x => x.NumberOfBeds)
                    .IsRequired();
                builder.Property(x => x.Price)
                    .IsRequired().HasColumnType("decimal(18,2)");
                builder.Property(x => x.HotelId)
                      .IsRequired();
                builder.HasOne(c => c.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(c => c.HotelId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasQueryFilter(p => !p.IsDeleted);
            }
        }
    }

}