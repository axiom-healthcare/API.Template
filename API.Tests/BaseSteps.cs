using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Newtonsoft.Json;
using Data.Components;

namespace Service.Rest.Tests
{
    public static class Extentions
    {
        public static void Remove(this ServiceDescriptor serviceDescriptor, IServiceCollection services) => 
            services.Remove(serviceDescriptor);

        public static void Remove<T>(this IServiceCollection services) => 
            services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T))?.Remove(services);
    }


    public class BaseSteps
    {
        public static T Deserialize<T>(string content) => 
            JsonConvert.DeserializeObject<T>(content);

        public static WebApplicationFactory<Startup> GetAPI() => 
            new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(builder => 
            {
                builder.ConfigureServices(services =>
                {
                    services.Remove<DbContextOptions<Context>>();
                    services.AddDbContext<Context>(options => options.UseSqlServer(Config.GetConnectionString()));
                });
            });

        protected readonly HttpClient client;

        protected BaseSteps() {
            var api = GetAPI();
            client = api.CreateClient();
            client.BaseAddress = new System.Uri("https://localhost:44329");
        }
    }
}
