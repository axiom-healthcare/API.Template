using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
    }

    /// <summary>
    /// Class used by Entity Framework Migrations at design time.
    /// </summary>
    public class DbFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            return new Context(Config.DbOptions());
        }
    }
}
