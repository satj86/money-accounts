using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace banana.tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Is_test_environment()
        {
            var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Assert.AreEqual("Test", env);
        }
    }
}
