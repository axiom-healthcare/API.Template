using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Data.Models;

namespace Service.Rest.Tests.Steps
{
    [Binding]
    public class EntitiesSteps : DataSteps
    {
        private HttpResponseMessage _response;
        private int _Id;

        [When(@"I send a POST request with header content type and body to \./Entities")]
        public async Task SendPOSTRequest()
        {
            var newEntity = JsonConvert.SerializeObject(new { Name = "NewEntity" });
            var body = new StringContent(newEntity, Encoding.UTF8, "application/json");
            _response = await client.PostAsync(Routes.Entities.Create(), body);
        }

        [Then(@"an CREATED HTTP Code should be returend")]
        public void ReceiveCREATED()
        {
            _response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Created);
        }

        [Then(@"the response body contains the newly created Entity")]
        public async Task ReceiveNewEntity()
        {
            var body = await _response.Content.ReadAsStringAsync();
            var entity = Deserialize<Entity>(body);

            data.Entities
                .First(entity => entity.Name == "NewEntity")
                .Should()
                .BeEquivalentTo(entity);
        }

        [When(@"I send a GET Request to \./Entities")]
        public async Task SendGetAllRequest()
        {
            _response = await client.GetAsync(Routes.Entities.Get());
        }

        [Then(@"an OK HTTP Code should be returned")]
        public void ReceiveOK()
        {
            _response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
        }

        [Then(@"the response body contains all Entities in Data Store")]
        public async Task ReceiveAllEntitiesInDataStore()
        {
            var content = await _response.Content.ReadAsStringAsync();
            var entities = Deserialize<List<Entity>>(content);

            data.Entities
                .ToList()
                .Should()
                .BeEquivalentTo(entities);
        }

        [Given(@"the Data Store contains an Entity")]
        public void DataStoreContainsAnEntity()
        {
            data.Entities
                .ToList()
                .Count
                .Should()
                .BeGreaterThan(0);
        }

        [When(@"I send a GET Request to \./Entities/ appended with a valid Entity Id")]
        public async Task SendGetRequest()
        {
            _Id = data.Entities
                .First(entity => entity.Name == "Entity")
                .Id;

            _response = await client.GetAsync(Routes.Entities.Get(_Id));
        }

        [Then(@"the response body contains the Entity with correct Id")]
        public async Task ReceiveEntityWithCorrectId()
        {
            var content = await _response.Content.ReadAsStringAsync();
            var entity = Deserialize<Entity>(content);

            data.Entities
                .First(entity => entity.Id == _Id)
                .Should()
                .BeEquivalentTo(entity);
        }

        [When(@"I send a PUT Request with header content type and body to \./Entitities/ appended with the Entity's Id")]
        public async Task SendPUTRequest()
        {
            var entity = data.Entities
                .First(entity => entity.Name == "Entity");

            var updatedEntity = new Entity()
            {
                Id = entity.Id,
                Name = "UpdatedEntity",
                Timestamp = entity.Timestamp
            };
       
            var stringEntity = JsonConvert.SerializeObject(updatedEntity);
            var body = new StringContent(stringEntity, Encoding.UTF8, "application/json");
            _response = await client.PutAsync(Routes.Entities.Update(entity.Id), body);
        }

        [Then(@"an NO CONTENT HTTP Code should be returned")]
        public void ReceiveNOCONTENT()
        {
            _response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.NoContent);
        }


        [When(@"I send a DELETE Reqeust to \./Entitites/ appended with the Entity's Id")]
        public async Task SendDeleteReqeust()
        {
            _Id = data.Entities.First().Id;
            _response = await client.DeleteAsync(Routes.Entities.Delete(_Id));
        }

        [Then(@"the Entity should not exist in Data Store")]
        public void ThenTheEntityShouldNotExistInDataStore()
        {
            data.Entities
                .Where(entity => entity.Id == _Id)
                .Should()
                .BeNullOrEmpty();
        }
    }
}