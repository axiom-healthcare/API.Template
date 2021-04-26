using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Newtonsoft.Json;
using Data.Components;

namespace Service.Rest.Tests
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Remove<T>(this IServiceCollection services)
        {
            var serviceDescriptor = services.First(descriptor => descriptor.ServiceType == typeof(T));
            if (serviceDescriptor != null) services.Remove(serviceDescriptor);

            return services;
        }
    }

    public class BaseSteps
    {
        public static T Deserialize<T>(string content) => 
            JsonConvert.DeserializeObject<T>(content);
        protected readonly HttpClient client;

        protected BaseSteps() {
            var api = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => {
                    builder.ConfigureServices(services =>
                    {
                        services.Remove<DbContextOptions<Context>>();
                        services.AddDbContext<Context>(options => options.UseSqlServer(Config.GetConnectionString()));
                    });
                });
            client = api.CreateClient();
            client.BaseAddress = new System.Uri("https://localhost:44329");
        }
    }
}
