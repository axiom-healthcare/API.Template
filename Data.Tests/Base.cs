using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.IO;

namespace Data.Tests
{
    public enum DataStoreType
    {
        InMemory,
        Sqlite
    }
    public class Base: IDisposable
    {
        private DbConnection _connection;

        public Context CreateDbContext(DataStoreType type)
        {
            switch (type)
            {
                case DataStoreType.InMemory:
                    return CreateInMemoryContext();
                case DataStoreType.Sqlite:
                    return CreateSqliteContext();
                default:
                    return CreateInMemoryContext();
            }
        }
        public Context CreateSqliteContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();
            }

            var options = new DbContextOptionsBuilder<Context>()
                .UseSqlite(_connection)
                .Options;

            using (Context context = new Context(options))
            {
                context.Database.EnsureCreated();
            }
         
            return new Context(options);
        }

        public Context CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new Context(options);
        }
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
