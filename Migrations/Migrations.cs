using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Migrations
{
    /// <summary>
    /// Class used by Entity Framework Migrations at DESIGN TIME ONLY.
    /// </summary>
    public class DbFactory : IDesignTimeDbContextFactory<Context>
    {
        /// <summary>
        /// Returns the connection string to an SQL Local DB.
        /// </summary>
        /// <param name="MigrationsProject">Name of Migrations Project</param> 
        public static string GetConnectionString() => new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("Data");

        /// <summary>
        /// Returns the SQL DB configuration
        /// </summary>
        /// <param name="MigrationsProject">Name of Migrations Project</param> 
        public static DbContextOptions<Context> GetDbOptions(string MigrationsProject) =>
            new DbContextOptionsBuilder<Context>()
                .UseSqlServer(GetConnectionString(), x => x.MigrationsAssembly(typeof(DbFactory).Assembly.FullName))
                .Options;

        public Context CreateDbContext(string[] args) => new(GetDbOptions("Migrations"));
    }

}
