using System.Linq;
using Xunit;

namespace Data.Tests
{
    public class Entity : Base
    {
        [Fact]
        public void ShouldFindOneEntity()
        {
            UseSqlite();
            var data = InitAndGetDbContext();

            Assert.Equal(1, data.Entities.Count(release => release.Name == "Test"));

        }
        private Context InitAndGetDbContext()
        {
            var data = GetDbContext();

            // Use to seed DB before testing
            data.Entities.Add(new Models.Entity()
            {
                Name = "Test"
            });
            data.SaveChanges();
            return data;
        }
    }
}
