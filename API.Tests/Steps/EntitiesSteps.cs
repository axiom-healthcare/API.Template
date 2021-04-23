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

        [Before]
        public void Before()
        {
            data = Config.CreateSQLContext();
        }

        [Given(@"An Entity was added to the Data Store")]
        public void AnEntityWasAddedToDataStore()
        {
            data.Entities.Add(new Entity()
            {
                Name = "Test"
            });
            data.SaveChanges();
        }

        [When(@"I send a HTTP GET Request to \./Entities")]
        public async Task WhenISendAHTTPGETRequestTo_EntitiesAsync()
        {
            response = await client.GetAsync(Routes.Entities.GetAll);
        }
        
        [Then(@"An HttpStatusCode\.Ok should be returned")]
        public void ThenTheIShouldHttpStatusCode_OkShouldBeReturned()
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"one Entity should be returned")]
        public async Task OneEnityShoulsBeReturned()
        {
            var entities = await response.Content.ReadAsStringAsync();
            var entityCount = JsonConvert.DeserializeObject<Entity[]>(entities).Length;

            entityCount.Should().Be(1);
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
