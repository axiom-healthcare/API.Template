using FluentAssertions;
using Xunit;

namespace Data.Tests
{
    public class DataStore : Base
    {
        [Fact]
        public void ShouldBeAbleToUseSqServerDb()
        {
            //Arrange


            //Act
            using var data = CreateDbContext(DataStoreType.SqlServer);

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.SqlServer");
        }
        [Fact]
        public void ShouldBeAbleToUseSqLiteDb()
        {
            //Arrange

            //Act
            var data = CreateDbContext(DataStoreType.Sqlite);

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.Sqlite");
        }
        [Fact]
        public void ShouldBeAbleToUseInMemoryDb()
        {
            //Arrange

            //Act
            var data = CreateDbContext(DataStoreType.InMemory);

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.InMemory");
        } 
    }
}
