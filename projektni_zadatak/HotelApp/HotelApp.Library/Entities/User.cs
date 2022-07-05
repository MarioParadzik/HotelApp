using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Api.Entities
{
    public class User : IdentityUser<int>, ISoftDeletable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<HotelUser> HotelUsers { get; set; }
        public bool IsDeleted { get; set; }

        private class UserBuilder : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.ToTable(nameof(User));
                builder.Property(x => x.FirstName)
                    .IsRequired();
                builder.Property(x => x.LastName)
                    .IsRequired();
                builder.HasQueryFilter(p => !p.IsDeleted);
            }
        }
    }
}
