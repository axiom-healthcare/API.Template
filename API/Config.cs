using Microsoft.Extensions.Configuration;
using System.IO;

namespace API
{
    public class Config
    {
        /// <summary>
        /// Returns the connection string to Database configured in the appsettings.json.
        /// TODO: Setup appsetings.developement, .staging, .production
        /// </summary> 
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