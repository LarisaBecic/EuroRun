using EuroRunAPI.Modul;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Authentification.Models;
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

        public DbSet<Award> Awards { get; set; }

        public DbSet<Challenge> Challenges { get; set; }

        public DbSet<ChallengeVerification> ChallengeVerifications { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<EventRegistration> EventRegistrations { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<ChallengeProgress> ChallengeProgresses { get; set; }

        public DbSet<AuthentificationToken> AuthentificationTokens { get; set; }




        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventRegistration>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.User_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventRegistration>()
                .HasOne(e => e.Event)
                .WithMany()
                .HasForeignKey(e => e.Event_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChallengeVerification>()
                .HasOne(cv => cv.Event)
                .WithMany()
                .HasForeignKey(cv => cv.Event_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChallengeVerification>()
                .HasOne(cv => cv.ChallengeProgress)
                .WithMany()
                .HasForeignKey(cv => cv.ChallengeProgress_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.User_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Event)
                .WithMany() 
                .HasForeignKey(r => r.Event_id)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete
        }

       

    }
}
