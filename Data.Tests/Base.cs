using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Data.Tests
{
    public enum DataStoreType
    {
        InMemory,
        Sqlite
    }
    public class Base
    {
        public Context GetDbContext(DataStoreType type)
        {
            switch (type)
            {
                case DataStoreType.InMemory:
                    return this.GetInMemoryDbContext();
                case DataStoreType.Sqlite:
                    return this.GetSqliteDbContext();
                default:
                    return this.GetInMemoryDbContext();
            }
        }

        private Context GetInMemoryDbContext()
        {
            var builder = new DbContextOptionsBuilder<Context>();
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new Context(builder.Options);
                dbContext.Database.EnsureCreated();
            return dbContext;
        }
        private Context GetSqliteDbContext()
        {
            var builder = new DbContextOptionsBuilder<Context>();
                builder.UseSqlite("DataSource=:memory:", x => { });
            
            var dbContext = new Context(builder.Options);
                dbContext.Database.OpenConnection();
                dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
