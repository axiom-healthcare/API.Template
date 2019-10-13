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
    }

    /// <summary>
    /// Class used by Entity Framework Migrations at design time.
    /// </summary>
    public class DbFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            // Name of Migrations Project passed in as parameter
            return new Context(Config.DbOptions("Migrations"));
        }
    }
}
