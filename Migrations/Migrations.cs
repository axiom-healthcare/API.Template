using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Data.Components;

namespace Migrations
{
    /// <summary>
    /// Class used by Entity Framework Migrations at DESIGN TIME ONLY.
    /// </summary>
    public class DbFactory : IDesignTimeDbContextFactory<Context>
    {
        /// <summary>
        /// Returns the connection string of Data Store.
        /// </summary>
        public static string GetConnectionString() => 
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("Data");

        /// <summary>
        /// Returns the SQL DB configuration
        /// </summary>
        public static DbContextOptions<Context> GetDbOptions() =>
            new DbContextOptionsBuilder<Context>()
                .UseSqlServer(GetConnectionString(), options =>
                    options
                        .MigrationsAssembly(typeof(DbFactory)
                        .Assembly
                        .FullName))
                .Options;

        public Context CreateDbContext(string[] args) => 
            new(GetDbOptions());
    }

}
