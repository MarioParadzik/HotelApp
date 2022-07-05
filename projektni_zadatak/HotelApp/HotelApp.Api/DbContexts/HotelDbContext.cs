using HotelApp.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HotelApp.Api.DbContexts
{
    public class HotelDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<PostOffice> PostOffices { get; set; }
        public DbSet<HotelStatus> HotelStatuses { get; set; }
        public DbSet<HotelUser> HotelUser { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<Configuration> Configurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
        public override int SaveChanges()
        {
            DeactivateEntities();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DeactivateEntities();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void DeactivateEntities()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Deleted && typeof(ISoftDeletable).IsAssignableFrom(entry.Entity.GetType()))
                {
                    entry.CurrentValues[nameof(ISoftDeletable.IsDeleted)] = true;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
