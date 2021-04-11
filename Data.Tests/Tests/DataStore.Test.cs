using FluentAssertions;
using NUnit.Framework;


namespace Data.Tests
{
    [TestFixture]
    class DataStore : Base
    {
        [Test]
        public void DefaultDataStoreIsSQLLocalDb()
        {
            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.SqlServer");
        }
        [Test]
        public void ShouldBeAbleToUseSqLiteDb()
        {
            //Act
            var data = Config.CreateSQLiteContext();

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.Sqlite");
        }
        [Test]
        public void ShouldBeAbleToUseInMemoryDb()
        {
            //Act
            var data = Config.CreateSQLInMemoryConext();

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.InMemory");
        }
    }
}
