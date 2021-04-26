using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Data.Components
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        // Example DbSet used for illustration purposes
        public DbSet<Entity> Entities { get; set; }

        // Data Store specific configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // SQL Lite configure Optimistic Concurrency 
            if (Database.IsSqlite())
            {
                modelBuilder
                    .Entity<Entity>()
                    .Property(e => e.Timestamp)
                    .IsRowVersion()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            }
        }
    }
}

