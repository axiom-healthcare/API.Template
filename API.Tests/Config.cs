using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace API.Tests
{
    public class Config
    {
        public static string GetConnectionString() => 
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("Test");
        
        public static DbContextOptions<Context> GetSQLContextOptions() => 
            new DbContextOptionsBuilder<Context>()
                   .UseSqlServer(Config.GetConnectionString())
                   .Options;

        public static Context CreateSQLContext() => 
            new Context(Config.GetSQLContextOptions());

    }
}
