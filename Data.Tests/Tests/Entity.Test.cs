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
            var name = "Name";
            data.Entities.Add(new Models.Entity() { Name = name });
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
            var name = "Name";
            data.Entities.Add(new Models.Entity() { Name = name });
            await data.SaveChangesAsync();

            //Act
            var entity = data.Entities.FirstOrDefault(entity => entity.Name == name);

            //Assert
            entity.Name.Should().BeOfType<string>();
        }

        /// <summary>
        /// Testing Optimistic Concurrency
        /// Updating of data via multiple web clients, at the sametime, may lead to wrong data in Data Store
        /// </summary>
        [Test]
        public async Task ExceptionShouldBeThrownWhenTryingToUpdateAnOutdatedEntity()
        {
            //Arrange
            var name = "Name";
            data.Entities.Add(new Models.Entity() { Name = name });
            await data.SaveChangesAsync();

            var entity = data.Entities.FirstOrDefault(entity => entity.Name == name);
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

        /// <summary>
        /// Testing Optimistic Concurrency
        /// Updating of data via multiple web clients, at the sametime, may lead to wrong data in Data Store
        /// </summary>
        [Test]
        public async Task ExceptionShouldNotBeThrownWhenTryingToUpdateAnEntity()
        {
            //Arrange
            var name = "Name";
            data.Entities.Add(new Models.Entity() { Name = name });
            await data.SaveChangesAsync();

            var entity = data.Entities.FirstOrDefault(entity => entity.Name == name);
            var Id = entity.Id.ToString();

            DbUpdateConcurrencyException exception = null;

            //Act
            entity.Name = "UpdatedName";

            try
            {
                data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                exception = ex;
            }

            //Assert
            exception.Should().BeNull();
        }
    }
}