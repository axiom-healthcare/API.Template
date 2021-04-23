using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace API.Tests
{
    [SetUpFixture]
    public class Config
    {
        /// <summary>
        /// Returns the connection string to Database configured in the appsettings.json.
        /// TODO: Setup appsetings.developement, .staging, .production
        /// </summary> 
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


        /// <summary>
        /// Ensure SQL Database is delete and recreated before running any test
        /// </summary> 
        [OneTimeSetUp]
        public async Task SetUp()
        {
            using var context = Config.CreateSQLContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
    }
}
