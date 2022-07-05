using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HotelApp.Api.Entities
{
    public class Reservation : ISoftDeletable
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? Note { get; set; }
        public int UserId { get; set; }
        public int ReservationStatusId { get; set; }
        public int RoomId { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }
        public virtual User User { get; set; }
        public virtual Room Room { get; set; }
        public bool IsDeleted { get; set; }

        private class ReservationBuilder : IEntityTypeConfiguration<Reservation>
        {
            public void Configure(EntityTypeBuilder<Reservation> builder)
            {
                builder.ToTable(nameof(Reservation));
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id);
                builder.Property(x => x.DateCreated)
                    .IsRequired();
                builder.Property(x => x.DateFrom)
                    .IsRequired();
                builder.Property(x => x.DateTo)
                    .IsRequired();
                builder.Property(x => x.Note)
                      .IsRequired();
                builder.Property(x => x.UserId)
                      .IsRequired();
                builder.Property(x => x.ReservationStatusId)
                      .IsRequired();
                builder.HasOne(e => e.User);
                builder.HasOne(c => c.ReservationStatus)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(c => c.ReservationStatusId)
                    .IsRequired();
                builder.HasOne(e => e.Room)
                    .WithMany(r => r.Reservations)
                    .HasForeignKey(e => e.RoomId);
                builder.HasQueryFilter(p => !p.IsDeleted);
            }

        }
    }
}