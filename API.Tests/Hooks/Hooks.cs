using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Service.Rest.Tests
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            using var context = Config.CreateContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
    }
}
