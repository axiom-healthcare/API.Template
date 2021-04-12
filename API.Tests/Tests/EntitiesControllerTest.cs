using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace API.Tests
{
    [TestFixture]
    public class EntitiesControllerTest : IntergationTest
    {
        [Test]
        public async Task GetEntities_ReturnsStatusOK()
        {
            //Arrange

            //Act
            var response = await client.GetAsync(Routes.Entities.GetAll);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}
