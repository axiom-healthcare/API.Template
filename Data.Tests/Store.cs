using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Data
{
    public static class Store
    {
        private static Dictionary<Provider, Func<Context>> _providers = new Dictionary<Provider, Func<Context>>()
        {
            {Provider.InMemory, CreateSQLInMemoryContext },
            {Provider.SQLite, CreateSQLiteContext },
            {Provider.SQL, CreateSQLContext }
        };

        /// <summary>
        /// A subset of Datastore Providers for testing the Data Layer
        /// </summary>
        public enum Provider
        {
            InMemory,
            SQLite,
            SQL
        }

        public static Context CreateContext(Provider provider)
        {
            return _providers[provider]();
        }

        public static Context CreateSQLContext()
        {
            var connection = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("Test");

            var options = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(connection)
                .Options;

            return new Context(options);
        }

        public static Context CreateSQLiteContext()
        {
            var connection = new SqliteConnection("Data Source=Test;Mode=Memory;Cache=Shared");
            connection.Open();

            var options = new DbContextOptionsBuilder<Context>()
                .UseSqlite(connection)
                .Options;

            using Context context = new Context(options);
            context.Database.EnsureCreated();

            return new Context(options);
        }

        public static Context CreateSQLInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("Test")
                .Options;

            return new Context(options);
        }
    }
}
