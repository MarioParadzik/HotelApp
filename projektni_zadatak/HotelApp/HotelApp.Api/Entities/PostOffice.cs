using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Api.Entities
{
    public class PostOffice : ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalNumber { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
        public bool IsDeleted { get; set; }

        private class PostOfficenBuilder : IEntityTypeConfiguration<PostOffice>
        {
            public void Configure(EntityTypeBuilder<PostOffice> builder)
            {
                builder.ToTable(nameof(PostOffice));
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id);
                builder.Property(x => x.Name)
                    .IsRequired();
                builder.Property(x => x.PostalNumber)
                    .IsRequired();
                builder.HasQueryFilter(p => !p.IsDeleted);
            }
        }
    }


}