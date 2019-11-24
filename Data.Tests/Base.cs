using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.IO;

namespace Data.Tests
{
    public enum DataStoreType
    {
        InMemory,
        Sqlite,
        SqlServer
    }
    public class Base
    {
        private DbConnection _connection;
        private DbContextOptions<Context> CreateSqlServerOptions()
        {
            var connection = Config.GetConnectionString();

            return new DbContextOptionsBuilder<Context>()
                .UseSqlServer(connection)
                .Options;
        }
        private DbContextOptions<Context> CreateSQLiteOptions()
        {
            return new DbContextOptionsBuilder<Context>()
                .UseSqlite(_connection)
                .Options;
        }
        private DbContextOptions<Context> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
        private Context CreateSqlServerContext()
        {
            var options = CreateSqlServerOptions();
            using Context context = new Context(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return new Context(options);
        }
        private Context CreateSqliteContext()
        {
            if (_connection == null)
            {
                // TODO: Leverage shared data source: DataSource=data;mode=memory;cache=shared
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                var options = CreateSQLiteOptions();

                using Context context = new Context(options);
                context.Database.EnsureCreated();
            }

            return new Context(CreateSQLiteOptions());
        }
        private Context CreateInMemoryContext()
        {
            var options = CreateInMemoryOptions();
            return new Context(options);
        }
        public Context CreateDbContext(DataStoreType type)
        {
            return type switch
            {
                DataStoreType.InMemory => CreateInMemoryContext(),
                DataStoreType.Sqlite => CreateSqliteContext(),
                DataStoreType.SqlServer => CreateSqlServerContext(),
                _ => CreateInMemoryContext(),
            };
        }
    }
}
