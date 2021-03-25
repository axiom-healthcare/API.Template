using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Data.Client
{
    public class Config
    {
        // Connection string to database retrived from appsettings
        public static string GetConnectionString()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("Data");
        }

        // By default the local development environment make use of a SQL Localdb as Data Store.
        // To change to a different edition of MS SQL, update the connection string defined in appsettings
        public static DbContextOptions<Context> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<Context>()
                .UseSqlServer(Config.GetConnectionString())
                .Options;
        }
    }
}
