using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataContext
{
    public class PollDbContext : DbContext
    {
        public PollDbContext(DbContextOptions<PollDbContext> options) : base(options)
        {
        }

        public DbSet<Poll> Polls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Poll>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Poll>()
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Poll>()
                .Property(p => p.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
