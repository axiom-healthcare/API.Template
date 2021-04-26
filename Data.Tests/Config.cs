using NUnit.Framework;
using System.Threading.Tasks;

namespace Data.Tests
{
    [SetUpFixture]
    class Config
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
