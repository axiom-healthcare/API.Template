using Microsoft.EntityFrameworkCore;
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
        /// Returns the connection string to an SQL Local DB
        /// </summary>
        public static string GetConnectionString()
        {
            return new DbConnectionStringBuilder
                {
                    { "Data Source", @"(localdb)\Data" },
                    { "AttachDbFilename", Directory.GetCurrentDirectory() + @"\Data\Data.mdf" },
                    { "Integrated Security", true },
                    { "database", "Data" }
                }.ConnectionString;
        }

        /// <summary>
        /// Returns the SQL DB configuration
        /// </summary>
        public static DbContextOptions<Context> DbOptions()
        {
            return new DbContextOptionsBuilder<Context>()
                .UseSqlServer(Config.GetConnectionString())
                .Options;
        }
    }
}
