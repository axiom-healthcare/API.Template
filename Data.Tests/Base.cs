using NUnit.Framework;

namespace Data.Tests
{
    public class Base
    {
        protected Context data = null!;

        /// <summary>
        /// By default, the testing of the Data Layer is configured to use the same database as in used production: MS SQL
        /// However, increased performance of unit testing can be achieved using a different Data Store
        /// See the DataStore.Test for exmaples on how to use either an InMemory or SQLite Data Store 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            data = Store.CreateContext(Store.Provider.SQL);
        }

        [TearDown]
        public void TearDown()
        {
            if (data != null)
            {
                data.Dispose();
            }
        }
    }
}
