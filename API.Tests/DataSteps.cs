using Data;
using TechTalk.SpecFlow;

namespace API.Tests
{
    [Binding]
    public class DataSteps: BaseSteps
    {
        protected Context data = null!;

        [Before]
        public void Before()
        {
            data = Config.CreateContext();
        }

        [After]
        public void After()
        {
            if (data != null)
            {
                data.Dispose();
            }
        }
    }
}
