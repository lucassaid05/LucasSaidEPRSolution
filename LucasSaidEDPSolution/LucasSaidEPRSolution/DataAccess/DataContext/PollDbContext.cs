using Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataContext
{
    internal class PollDbContext : DbContext
    {
        public PollDbContext(DbContextOptions<PollDbContext> options) : base(options)
        {
        }

        public DbSet<Poll> Polls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
