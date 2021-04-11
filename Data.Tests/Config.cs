using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace Data.Tests
{
    public class Config
    {
        /// <summary>
        /// Returns the connection string to Database configured in the appsettings.json.
        /// TODO: Setup appsetings.developement, .staging, .production
        /// </summary> 
        public static string GetConnectionString()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("Test");
        }

        public static Context CreateContext()
        {
            var connection = Config.GetConnectionString();

            var options = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(connection)
                .Options;

            return new Context(options);
        }

        public static Context CreateSQLiteContext()
        {
            var connection = new SqliteConnection("Data Source=Test;Mode=Memory;Cache=Shared");
            connection.Open();

            var options = new DbContextOptionsBuilder<Context>()
                .UseSqlite(connection)
                .Options;

            using Context context = new Context(options);
            context.Database.EnsureCreated();

            return new Context(options);
        }

        public  static Context CreateSQLInMemoryConext()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("Test")
                .Options;

            return new Context(options);
        }
    }


    [SetUpFixture]
    class SetUpFixture
    {
        [OneTimeSetUp]
        public async Task SetUp()
        {
            using var context = Config.CreateContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
    }
}