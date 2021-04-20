using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO;

namespace Data
{
    /// <summary>
    /// Class used to configure Entity Framework Migrations.
    /// </summary>
    class Migrations
    {
        /// <summary>
        /// Returns the connection string to an SQL Local DB.
        /// </summary>
        /// <param name="MigrationsProject">Name of Migrations Project</param> 
        public static string GetConnectionString() =>  new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("Data");

        /// <summary>
        /// Returns the SQL DB configuration
        /// </summary>
        /// <param name="MigrationsProject">Name of Migrations Project</param> 
        public static DbContextOptions<Context> GetDbOptions(string MigrationsProject) => 
            new DbContextOptionsBuilder<Context>()
                .UseSqlServer(GetConnectionString(), x => x.MigrationsAssembly(MigrationsProject))
                .Options;
    }
}
