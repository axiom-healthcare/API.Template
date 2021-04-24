using Data.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace API.Tests.Steps
{
    [Binding]
    public class EntitiesSteps : DataSteps
    {
        private HttpResponseMessage _response;

        [Given(@"these Two Entities was added to the Data Store:")]
        public void TwoEntitiesAddedToDataStore(Table table)
        {
            table.CreateSet<Entity>()
                .ToList()
                .ForEach(entity => data.Entities.Add(entity));
  
            data.SaveChanges();
        }

        [When(@"I send a GET Request to \./Entities")]
        public async Task WhenGetEntities()
        {
            _response = await client.GetAsync(Routes.Entities.GetAll);
        }
        
        [Then(@"an Ok HTTP Status Code should be returned")]
        public void ThenOKCodeShouldBeReturned()
        {
            _response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
        }

        [Then(@"the two Entity should be returned")]
        public async Task ThenEntityShouldBeReturned()
        {
            var content = await _response.Content.ReadAsStringAsync();
            var entities = Deserialize<List<Entity>>(content);

            data.Entities
                .ToList()
                .Should()
                .BeEquivalentTo(entities);
        }
    }
}