using NUnit.Framework;
using System.Threading.Tasks;

namespace Data.Tests
{
    [SetUpFixture]
    class Startup
    {
        [OneTimeSetUp]
        public async Task Init()
        {
            await CleanDataStore(Store.Provider.SQL);
            await CleanDataStore(Store.Provider.SQLite);
        }

        /// <summary>
        /// Ensure clean Data Store by deleteing and recreating Database
        /// </summary>
        public static async Task CleanDataStore(Store.Provider provider)
        {
            using var context = Store.CreateContext(provider);
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
    }
}
