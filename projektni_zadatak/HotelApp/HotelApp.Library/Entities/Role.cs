using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Api.Entities
{
    public class Role : IdentityRole<int>
    {
        public const string SuperAdministrator = "Super Administrator";
        public const string Administrator = "Administrator";
        public const string HotelManager = "Hotel Manager";
        public const string RegisteredUser = "Registered User";

        private class RoleBuilder : IEntityTypeConfiguration<Role>
        {
            List<Role> roles = new List<Role>()
            {
                new Role { Id = 1, Name = SuperAdministrator, NormalizedName = SuperAdministrator.ToUpper() },
                new Role { Id = 2, Name = Administrator, NormalizedName = Administrator.ToUpper() },
                new Role { Id = 3, Name = HotelManager, NormalizedName = HotelManager.ToUpper() },
                new Role { Id = 4, Name = RegisteredUser, NormalizedName = RegisteredUser.ToUpper() }
            };

            public void Configure(EntityTypeBuilder<Role> builder)
            {
                builder.HasData(roles);
            }

        }
    }

}
