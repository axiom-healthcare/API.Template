using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Tests
{
    [TestFixture]
    public class Entity : Base
    {
        [Test]
        public async Task ShouldContainId()
        {
            //Arrange
            data.Entities.Add(new Models.Entity() { Name = "Name" });
            await data.SaveChangesAsync();

            //Act
            var entity = data.Entities.First();

            //Assert
            entity.Id.Should().BeOfType(typeof(int));
        }
        [Test]
        public async Task ShouldContainName()
        {
            //Arrange
            data.Entities.Add(new Models.Entity() { Name = "Name" });
            await data.SaveChangesAsync();

            //Act
            var entity = data.Entities.FirstOrDefault(entity => entity.Name == "Name");

            //Assert
            entity.Name.Should().BeOfType<string>();
        }
        [Test]
        public async Task ExceptionShouldBeThrownWhenTryingToUpdateAnOutdatedEntity()
        {
            //Arrange
            data.Entities.Add(new Models.Entity() { Name = "Name" });
            await data.SaveChangesAsync();

            var entity = data.Entities.FirstOrDefault(entity => entity.Name == "Name");
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