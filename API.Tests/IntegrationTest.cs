using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Data;
using NUnit.Framework;
using Data.Models;

namespace API.Tests
{
    public class IntergationTest
    {
        protected readonly HttpClient client;
        protected Context data = null!;

        protected IntergationTest() {
            var connection = Config.GetConnectionString();
            var APIFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(Context));
                        services.AddDbContext<Context>(options => options.UseSqlServer(connection));
                    });
                });
            client = APIFactory.CreateClient();
        }
    }
}
