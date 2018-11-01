using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Common;
using Open.Common.Utility;
using Open.Common.Configuration;

namespace Open.SPF.Core.Test
{
    /// <summary>
    /// Summary description for ConfigurationTests
    /// </summary>
    [TestClass]
    public class ConfigurationTests
    {
        public ConfigurationTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void ConfigurationLogSourceTest()
        {
            string result = CommonConfiguration.Item(CommonConstants.AppSettings.ApplicationLogSource);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
            Assert.IsTrue(result.Contains("Log Source"));
        }

        [TestMethod]
        public void ConfigurationDiagnosticLoggingLevelTest()
        {
            string result = CommonConfiguration.Item(CommonConstants.AppSettings.DiagnosticLoggingLevel);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
            Assert.AreEqual("Concise", result);
        }

        [TestMethod]
        public void ConfigurationServiceRegistrationTypeTest()
        {
            string result = CommonConfiguration.Item(CommonConstants.AppSettings.ServiceRegistrationType);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
            Assert.IsTrue(result.Contains("DependencyInjection.UnitTestServiceRegistration"));

            Type resultType = Type.GetType(result);

            Assert.IsNotNull(resultType);
            Assert.AreEqual(resultType.FullName, (typeof(Open.Common.Test.DependencyInjection.UnitTestServiceRegistration)).FullName);
        }


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void RunBeforeAllTests(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void RunAfterAllTests() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void RunBeforeEachTest() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void RunAfterEachTest() { }
        //
        #endregion
    }
}
