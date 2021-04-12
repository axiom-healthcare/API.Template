using FluentAssertions;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    class DataStore : Base
    {
        [Test]
        public void DefaultDataStoreIsSQL()
        {
            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.SqlServer");
        }
        [Test]
        public void ShouldBeAbleToUseSqLiteDb()
        {
            //Act
            var data = Store.CreateContext(Store.Provider.SQLite);

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.Sqlite");
        }
        [Test]
        public void ShouldBeAbleToUseInMemoryDb()
        {
            //Act
            var data = Store.CreateContext(Store.Provider.InMemory);

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.InMemory");
        }
    }
}
