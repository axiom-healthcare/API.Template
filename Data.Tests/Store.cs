using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using Data.Components;

namespace Data
{
    public static class Store
    {
        private static readonly Dictionary<Provider, Func<DbContextOptions<Context>>> _providers = new()
        {
            { Provider.InMemory, GetSQLInMemoryContextOptions },
            { Provider.SQLite, GetSQLiteDbContextOptions },
            { Provider.SQL, GetSQLDbContextOptions }
        };

        /// <summary>
        /// A subset of supported Data Store Providers for testing the Data Layer
        /// </summary>
        public enum Provider
        {
            InMemory,
            SQLite,
            SQL 
        }

        public static Context CreateContext(Provider provider) =>
            new Context(_providers[provider]());

        public static string GetSQLConnection() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("Test");

        public static SqliteConnection GetSqliteConnection() =>
            new SqliteConnection("Data Source=Test;Mode=Memory;Cache=Shared");

        public static DbContextOptions<Context> GetSQLDbContextOptions() =>
            new DbContextOptionsBuilder<Context>()
                .UseSqlServer(GetSQLConnection())
                .Options;

        public static DbContextOptions<Context> GetSQLInMemoryContextOptions() =>
            new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("Test")
                .Options;

        public static DbContextOptions<Context> GetSQLiteDbContextOptions() => 
            new DbContextOptionsBuilder<Context>()
                .UseSqlite(GetSqliteConnection())
                .Options; 
    }
}
