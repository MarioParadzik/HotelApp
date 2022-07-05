using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Api.Entities
{
    public class Configuration : ISoftDeletable
    {
        public const string Deadline = "Deadline";
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        private class ConfigurationBuilder : IEntityTypeConfiguration<Configuration>
        {
            public void Configure(EntityTypeBuilder<Configuration> builder)
            {
                builder.ToTable(nameof(Configuration));
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Key)
                    .IsRequired();
                builder.Property(x => x.Value)
                    .IsRequired();
                builder.Property(x => x.Type)
                    .IsRequired();
                builder.Property(x => x.Description)
                    .IsRequired();
                builder.HasQueryFilter(p => !p.IsDeleted);
            }
        }
    }
    
}
