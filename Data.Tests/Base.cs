using NUnit.Framework;


namespace Data.Tests
{
    public class Base
    {
        protected Context data = null!;

        [SetUp]
        public void Setup()
        {
            data = Config.CreateContext();
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
