using Data;
using Data.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace API.Tests.Steps
{
    [Binding]
    public class EntitiesSteps: IntergationTest
    {
        protected new Context data = null!;
        private HttpResponseMessage response;
        private Entity entity;

        [Before]
        public void Before()
        {
            data = Config.CreateContext();
        }

        [Given(@"An Entity was added to the Data Store")]
        public void AnEntityWasAddedToDataStore()
        {
            entity = new Entity()
            {
                Name = "Test"
            };

            data.Entities.Add(entity);
            data.SaveChanges();
        }

        [When(@"I send a GET Request to \./Entities")]
        public async Task WhenGetEntities()
        {
            response = await client.GetAsync(Routes.Entities.GetAll);
        }
        
        [Then(@"An Ok HTTP Status Code should be returned")]
        public void ThenOKCodeShouldBeReturned()
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"The Entity should be returned")]
        public async Task ThenThenEntityShouldBeReturned()
        {
            var content = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<Entity[]>(content);
            var count = entities.Length;

            count.Should().Be(1);
            entities[0].Id.Should().Be(entity.Id);
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
