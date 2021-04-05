using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Data.Models;
using Microsoft.Extensions.Logging;


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
    /// Class used by Entity Framework Migrations at design time.
    /// </summary>
    public class DbFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            // Name of Migrations Project
            return new Context(Config.DbOptions("Migrations"));
        }
    }
}
