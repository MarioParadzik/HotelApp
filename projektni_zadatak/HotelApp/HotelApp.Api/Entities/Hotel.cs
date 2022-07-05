using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Api.Entities
{
    public class Hotel : ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactMail { get; set; }
        public string ContactNumber { get; set; }
        public string Adress { get; set; }
        public int LocationId { get; set; }
        public int HotelStatusId { get; set; }


        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<User> HotelManagers { get; set; }
        public virtual Location Location { get; set; }
        public virtual HotelStatus HotelStatus { get; set; }
        public bool IsDeleted { get; set; }

        private class HotelBuilder : IEntityTypeConfiguration<Hotel>
        {
            public void Configure(EntityTypeBuilder<Hotel> builder)
            {
                builder.ToTable(nameof(Hotel));
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id);
                builder.Property(x => x.Name)
                    .IsRequired();
                builder.Property(x => x.ContactMail)
                    .IsRequired();
                builder.Property(x => x.ContactNumber)
                    .IsRequired();
                builder.Property(x => x.Adress)
                      .IsRequired();
                builder.Property(x => x.LocationId)
                      .IsRequired();
                builder.Property(x => x.HotelStatusId)
                      .IsRequired();
                builder.HasOne(c => c.Location)
                        .WithMany(p => p.Hotels)
                        .HasForeignKey(k => k.LocationId);
                builder.HasOne(c => c.HotelStatus)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(k => k.HotelStatusId);
                builder.HasQueryFilter(p => !p.IsDeleted);
            }
        }
    }

   
}
