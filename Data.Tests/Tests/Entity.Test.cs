using System.Linq;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests
{
    public class Entity: Base
    {
        [Fact]
        public async void ShouldContainId()
        {
            //Arrange
            using var data = CreateDbContext(DataStoreType.Sqlite);
            data.Entities.Add(new Models.Entity() { Name = "Name" });
            await data.SaveChangesAsync();

            //Act
            var entity = data.Entities.First();

            //Assert
            entity.Id.Should().BeOfType(typeof(int));
        }
        [Fact]
        public async void ShouldContainName()
        {
            //Arrange
            using var data = CreateDbContext(DataStoreType.Sqlite);
            data.Entities.Add(new Models.Entity() { Name = "Name" });
            await data.SaveChangesAsync();

            //Act
            var entity = data.Entities.First();

            //Assert
            entity.Name.Should().BeOfType<string>();
        }
        [Fact]
        public async void ExceptionShouldBeThrownWhenTryingToUpdateAnOutdatedEntity()
        {
            //Arrange
            using var data = CreateDbContext(DataStoreType.Sqlite);
            data.Entities.Add(new Models.Entity() { Name = "Name" });
            await data.SaveChangesAsync();

            var entity = data.Entities.First();
            var Id = entity.Id.ToString();

            DbUpdateConcurrencyException exception = null;

            //Act
            entity.Name = "UpdatedName";
            data.Database.ExecuteSqlRaw("UPDATE Entities SET Name = 'Test3' WHERE Id = " + Id);
                
            try
            {
                data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                exception = ex;
            }

            //Assert
            exception.Should().NotBeNull();
        }
    }
}
