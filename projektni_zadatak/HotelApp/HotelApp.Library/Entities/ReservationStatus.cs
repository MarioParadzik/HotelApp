using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Api.Entities
{
    public class ReservationStatus : ISoftDeletable
    {
        public const int InProcess = 1;
        public const int Accepted = 2;
        public const int Rejected = 3;
        public const int Canceled = 4;
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public bool IsDeleted { get; set; }

        private class ReservationStatusBuilder : IEntityTypeConfiguration<ReservationStatus>
        {
            public void Configure(EntityTypeBuilder<ReservationStatus> builder)
            {
                builder.ToTable(nameof(ReservationStatus));
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).IsRequired();
                builder.Property(x => x.Name).IsRequired();
                builder.HasData(new ReservationStatus
                {
                    Id = InProcess,
                    Name = nameof(InProcess),
                },
                new ReservationStatus
                {
                    Id = Accepted,
                    Name = nameof(Accepted),
                },
                new ReservationStatus
                {
                    Id = Rejected,
                    Name = nameof(Rejected),
                },
                new ReservationStatus
                {
                    Id = Canceled,
                    Name = nameof(Canceled),
                });
                builder.HasQueryFilter(p => !p.IsDeleted);
            }
        }
    }
}