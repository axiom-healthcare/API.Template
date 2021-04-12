using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace Data
{
    namespace Tests
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
        }

        [SetUpFixture]
        class SetUpFixture
        {
            /// <summary>
            /// Ensure SQL Database is delete and recreated before running any test
            /// </summary> 
            [OneTimeSetUp]
            public async Task SetUp()
            {
                using var context = Store.CreateContext(Store.Provider.SQL);
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            }
        }

    }   
}