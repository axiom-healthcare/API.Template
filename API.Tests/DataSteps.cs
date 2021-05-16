using TechTalk.SpecFlow;
using Data.Components;
using Data.Models;

namespace Service.Rest.Tests
{
    [Binding]
    public class DataSteps: BaseSteps
    {
        protected Context data = null!;

        [Before]
        public void Before()
        {
            data = Config.CreateContext();

            var entity = new Entity()
            {
                Name = "Entity"
            };

            data.Entities.Add(entity);
            data.SaveChanges();
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
