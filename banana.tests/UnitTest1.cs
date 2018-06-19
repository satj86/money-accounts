using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace banana.tests
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }
        

        [TestMethod]
        public void Is_test_environment()
        {

            var env = TestContext.Properties["environment"].ToString();

            var secretKey = TestContext.Properties["secret"].ToString();
            Assert.AreEqual("Test", env);
            Assert.AreEqual("shhhh", secretKey);
        }
    }
}
