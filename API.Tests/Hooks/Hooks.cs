using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace API.Tests.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            using var context = Config.CreateSQLContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
    }
}
