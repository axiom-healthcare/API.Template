using System.Linq;
using Xunit;
using FluentAssertions;

namespace Data.Tests
{
    public class Entity: Base
    {
        [Fact]
        public void ShouldContainId()
        {
            //Arrange
            var data = CreateDbContext(DataStoreType.Sqlite);
            string name = "Test";

            //Act
            data.Entities.Add(new Models.Entity()
            {
                Name = name
            });
            data.SaveChanges();

            //Assert
            data.Entities.Count(entity => entity.Name == name).Should().Be(1);
        }
    }
}
