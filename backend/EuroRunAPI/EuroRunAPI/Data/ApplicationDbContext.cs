using EuroRunAPI.Modul.Models;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Administrator> Administrators { get; set; }

        public DbSet<EventType> EventTypes { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Race> Races { get; set; }







        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
