using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class EntitiesControllerTest : IntergationTest
    {
        [Fact]
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
