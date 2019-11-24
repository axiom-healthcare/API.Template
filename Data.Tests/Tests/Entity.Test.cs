using System.Linq;
using Xunit;
using FluentAssertions;

namespace Data.Tests
{
    public class Entity: Base
    {
        [Fact]
        public async void ShouldContainId()
        {
            //Act
            var name = "Test";
            using var data = CreateDbContext(DataStoreType.Sqlite);
            data.Entities.Add(new Models.Entity() { Name = name });
            await data.SaveChangesAsync();

            //Arrange
            var entity = data.Entities.First();

            //Assert
            entity.Id.Should().BeOfType(typeof(int));
        }
        [Fact]
        public async void ShouldContainName()
        {
            //Act
            var name = "Test";
            using var data = CreateDbContext(DataStoreType.Sqlite);
            data.Entities.Add(new Models.Entity() { Name = name });
            await data.SaveChangesAsync();

            //Arrange
            var entity = data.Entities.First();

            //Assert
            entity.Name.Should().BeOfType<string>();
        }
    }
}
