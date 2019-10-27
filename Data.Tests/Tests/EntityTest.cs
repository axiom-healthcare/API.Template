using System.Linq;
using Xunit;

namespace Data.Tests
{
    public class EntityTest : Base
    {
        [Fact]
        public void ShouldFindOneEntity()
        {
            //Arrange

            //Act
            var data = InitAndGetDbContext(DataStoreType.Sqlite);

            //Assert
            Assert.Equal(1, data.Entities.Count(release => release.Name == "Test"));
        }
        private Context InitAndGetDbContext(DataStoreType type)
        {
            var data = GetDbContext(type);

            // Seed DB before testing
            data.Entities.Add(new Models.Entity()
            {
                Name = "Test"
            });
            data.SaveChanges();
            return data;
        }
    }
}
