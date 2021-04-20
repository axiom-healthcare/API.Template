using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Data.Models;

namespace Data
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

    /// <summary>
    /// Class used by Entity Framework Migrations at DESIGN TIME ONLY.
    /// </summary>
    public class DbFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args) => new(Migrations.GetDbOptions("Migrations"));
    }
}

