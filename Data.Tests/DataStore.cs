using Xunit;

namespace Data.Tests
{
    public class DataStore : Base
    {
        [Fact]
        public void ShouldBeAbleToUseSqLiteDb()
        {
            UseSqlite();
            var data = InitAndGetDbContext();
            Assert.Equal("Microsoft.EntityFrameworkCore.Sqlite", data.Database.ProviderName);

        }
        [Fact]
        public void ShouldBeAbleToUseInMemoryDb()
        {
            var data = InitAndGetDbContext();
            Assert.Equal("Microsoft.EntityFrameworkCore.InMemory", data.Database.ProviderName);

        }

        private Context InitAndGetDbContext()
        {
            var data = GetDbContext();

            // Use to seed DB before testing
            return data;
        }
    }
}
