using Data;
using Data.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace API.Tests
{
    [TestFixture]
    public class EntitiesControllerTest : IntergationTest
    {
        private new Context data = null!;

        [SetUp]
        public void Setup()
        {
            data = Config.CreateSQLContext();
        }

        [Test]
        public async Task GetEntitiesReturnsStatusOK()
        {
            //Arrange

            //Act
            var response = await client.GetAsync(Routes.Entities.GetAll);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetEntitiesReturnsAllEntitiesInDataStore()
        {
            //Arrange
            data.Entities.Add(new Entity()
            {
                Name = "Test"
            });
            data.SaveChanges();

            //Act
            var response = await client.GetAsync(Routes.Entities.GetAll);
            var entities = await response.Content.ReadAsStringAsync();
            var entityCount = JsonConvert.DeserializeObject<Entity[]>(entities).Length;

            //Assert
            entityCount.Should().Be(1);
        }

        [TearDown]
        public void TearDown()
        {
            if (data != null)
            {
                data.Dispose();
            }
        }
    }
}
