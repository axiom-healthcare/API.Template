using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Data.Tests
{
    public enum Provider
    {
        InMemory,
        Sqlite,
        SqlServer
    }

    public class Base
    {
        private Dictionary<Provider, Func<Context>> _providers = new Dictionary<Provider, Func<Context>>();

        public Base()
        {
            _providers.Add(Provider.InMemory, CreateInMemoryContext);
            _providers.Add(Provider.Sqlite, CreateSqliteContext);
            _providers.Add(Provider.SqlServer, CreateSqlServerContext);
        }
  
        private Context CreateSqlServerContext()
        {
            var connection = Config.GetConnectionString();

            var options = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(connection)
                .Options;
        
            using Context context = new Context(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return new Context(options);
        }
        private Context CreateSqliteContext()
        {
            var connection = new SqliteConnection("Data Source=Test;Mode=Memory;Cache=Shared");
            connection.Open();

            var options  = new DbContextOptionsBuilder<Context>()
                .UseSqlite(connection)
                .Options;

            using Context context = new Context(options);
            context.Database.EnsureCreated();

            return new Context(options);
        }
        private Context CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("Test")
                .Options;

            return new Context(options);
        }
        
        public Context CreateDbContext(Provider provider = Provider.InMemory)
        {
            return _providers[provider]();
        }
    }
}
