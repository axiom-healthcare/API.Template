using Microsoft.EntityFrameworkCore;
using System;


namespace Data.Tests
{
    public class Base
    {
        private bool useSqlite = false;
        public Context GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<Context>();
            if (useSqlite)
            {
                // Use Sqlite DB.
                builder.UseSqlite("DataSource=:memory:", x => { });
            }
            else
            {
                // Use In-Memory DB.
                var ID = Guid.NewGuid().ToString();
                builder.UseInMemoryDatabase(ID);
            }

            var dbContext = new Context(builder.Options);
            if (useSqlite)
            {
                // SQLite needs to open connection to the DB.
                dbContext.Database.OpenConnection();
            }

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        public void UseSqlite()
        {
            useSqlite = true;
        }
    }
}
