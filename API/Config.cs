using Microsoft.Extensions.Configuration;
using System.IO;

namespace API
{
    public class Config
    {
        // Connection string to database retrived from appsettings
        public static string GetConnectionString()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("Data");
        }
    }
}