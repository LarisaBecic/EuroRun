using EuroRunAPI.Modul.Models;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Grad> Gradovi { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
