using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.IO;

namespace Data
{
    /// <summary>
    /// Class used to configure Entity Framework Migrations.
    /// </summary>
    class Config
    {
        /// <summary>
        /// Returns the connection string to an SQL Local DB.
        /// </summary>
        /// <param name="MigrationsProject">Name of Migrations Project</param> 
        public static string GetConnectionString()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("Data");
        }

        /// <summary>
        /// Returns the SQL DB configuration
        /// </summary>
        /// <param name="MigrationsProject">Name of Migrations Project</param> 
        public static DbContextOptions<Context> DbOptions(string MigrationsProject)
        {
            return new DbContextOptionsBuilder<Context>()
                .UseSqlServer(Config.GetConnectionString(), x => x.MigrationsAssembly(MigrationsProject))
                .Options;
        }
    }
}
