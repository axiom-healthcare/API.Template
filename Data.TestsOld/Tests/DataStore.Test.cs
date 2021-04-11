using FluentAssertions;
using Xunit;

namespace Data.TestsOld
{
    public class DataStore : Base
    {
        [Fact]
        public void ShouldBeAbleToUseSqServerDb()
        {
            //Arrange


            //Act
            using var data = CreateDbContext(Provider.SqlServer);

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.SqlServer");
        }
        [Fact]
        public void ShouldBeAbleToUseSqLiteDb()
        {
            //Arrange

            //Act
            var data = CreateDbContext(Provider.Sqlite);

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.Sqlite");
        }
        [Fact]
        public void ShouldBeAbleToUseInMemoryDb()
        {
            //Arrange

            //Act
            var data = CreateDbContext(Provider.InMemory);

            //Assert
            data.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.InMemory");
        } 
    }
}
