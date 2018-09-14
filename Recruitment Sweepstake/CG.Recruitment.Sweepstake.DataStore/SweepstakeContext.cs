// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The Contact Group">2018</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CG.Recruitment.Sweepstake.DataStore
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// This class is a Microsoft.EntityFrameworkCore DbContext.
    /// </summary>
    /// <seealso cref="DbContext" />
    public class SweepstakeContext : DbContext
    {
        public SweepstakeContext(DbContextOptions<SweepstakeContext> options) : base(options)
        {
        }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<Competitor> Competitors { get; set; }
        
        public DbSet<Gambler> Gamblers { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
            .ToTable("Message", "Sweepstake");

            modelBuilder.Entity<Message>()
                .HasOne(m => m.FromGambler)
                .WithOne()
                .HasForeignKey<Gambler>(m => m.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.ToGambler)
                .WithOne()
                .HasForeignKey<Gambler>(m => m.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Competition>()
                .ToTable("Competition", "Sweepstake");

            modelBuilder.Entity<Competitor>()
                .ToTable("Competitor", "Sweepstake");

            modelBuilder.Entity<Gambler>()
                .ToTable("Gambler", "Sweepstake");

            modelBuilder.Entity<Ticket>()
                .ToTable("Ticket", "Sweepstake");


            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Gambler)
                .WithMany(g => g.Tickets)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Competition)
                .WithMany(c => c.Tickets)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Competitor)
                .WithMany(c => c.Tickets)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}