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
        /// Returns the connection string to an SQL Local DB.
        /// </summary>
        /// <param name="MigrationsProject">Name of Migrations Project</param> 
        public static string GetConnectionString(string MigrationsProject)
        {
            var SolutionPath = Directory.GetParent(Directory.GetCurrentDirectory());
            var DbFilePath = @"\" + MigrationsProject + @"\Data\Data.mdf";

            return new DbConnectionStringBuilder
                {
                    { "Data Source", @"(localdb)\Data" },
                    { "AttachDbFilename", SolutionPath + DbFilePath },
                    { "Integrated Security", true },
                    { "database", "Data" }
                }.ConnectionString;
        }

        /// <summary>
        /// Returns the SQL DB configuration
        /// </summary>
        /// <param name="MigrationsProject">Name of Migrations Project</param> 
        public static DbContextOptions<Context> DbOptions(string MigrationsProject)
        {
            return new DbContextOptionsBuilder<Context>()
                .UseSqlServer(Config.GetConnectionString(MigrationsProject), x => x.MigrationsAssembly(MigrationsProject))
                .Options;
        }
    }
}
