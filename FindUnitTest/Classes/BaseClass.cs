using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FindUnitTest.Classes
{
    public class BaseClass
    {

        [TestInitialize]
        public void SetupTestBase()
        {

        }

        [TestCleanup]
        public void TeardownTestBase()
        {

        }  
        protected TestContext TestContextInstance;
        public TestContext TestContext
        {
            get => TestContextInstance;
            set => TestContextInstance = value;
        }

    }
}