using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Tests.Tests
{
    [TestFixture]
    class DataStore : Base
    {
        [Test]
        public async Task DefaultDataStoreIsSQLLocalDb()
        {
            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.SqlServer");
        }
        [Test]
        public async Task ShouldBeAbleToUseSqLiteDb()
        {
            //Arrange
            var connection = new SqliteConnection("Data Source=Test;Mode=Memory;Cache=Shared");
            connection.Open();

            var options = new DbContextOptionsBuilder<Context>()
                .UseSqlite(connection)
                .Options;

            using Context context = new Context(options);
            await context.Database.EnsureCreatedAsync();
   
            //Act
            var data = new Context(options);

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.Sqlite");
        }
        [Test]
        public void ShouldBeAbleToUseInMemoryDb()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("Test")
                .Options;

            //Act
            var data = new Context(options);

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.InMemory");
        }
    }
}
