using FluentAssertions;
using Xunit;

namespace Data.Tests
{
    public class DataStoreTest : Base
    {
        [Fact]
        public void ShouldBeAbleToUseInMemoryDb()
        {
            //Arrange

            //Act
            var data = CreateDbContext(DataStoreType.InMemory);

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.InMemory");
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
    }
}
