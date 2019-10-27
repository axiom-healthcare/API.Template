using System;
using Xunit;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace API.Tests
{
    public class IntergationTest
    {
        protected readonly HttpClient client;

        protected IntergationTest() {
            var APIFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(Context));
                        services.AddDbContext<Context>(options => options.UseInMemoryDatabase("IntegrationTestsDb"));
                    });
                });
            client = APIFactory.CreateClient();
        }

    }
}
