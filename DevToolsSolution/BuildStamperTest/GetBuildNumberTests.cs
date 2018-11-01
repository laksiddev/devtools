using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildStamper;

namespace BuildStamperTest
{
    /// <summary>
    /// Summary description for BuildStamperUtilityTests
    /// </summary>
    [TestClass]
    public class GetBuildNumberTests
    {
        public GetBuildNumberTests()
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
        public void GetBuildNumberTest_2010_012()
        {
            DateTime testTime = new DateTime(2010, 1, 12, 0, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)10012, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2010_024()
        {
            DateTime testTime = new DateTime(2010, 1, 24, 1, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);


            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)10024, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2010_043()
        {
            DateTime testTime = new DateTime(2010, 2, 12, 2, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);


            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)10043, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2010_055()
        {
            DateTime testTime = new DateTime(2010, 2, 24, 3, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);


            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)10055, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2011_071()
        {
            DateTime testTime = new DateTime(2011, 3, 12, 4, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);


            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)11071, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2011_083()
        {
            DateTime testTime = new DateTime(2011, 3, 24, 5, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)11083, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2011_102()
        {
            DateTime testTime = new DateTime(2011, 4, 12, 6, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)11102, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2011_114()
        {
            DateTime testTime = new DateTime(2011, 4, 24, 7, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)11114, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2012_133()
        {
            DateTime testTime = new DateTime(2012, 5, 12, 8, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)12133, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2012_145()
        {
            DateTime testTime = new DateTime(2012, 5, 24, 9, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)12145, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2013_163()
        {
            DateTime testTime = new DateTime(2013, 6, 12, 10, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)13163, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2013_175()
        {
            DateTime testTime = new DateTime(2013, 6, 24, 11, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)13175, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2014_193()
        {
            DateTime testTime = new DateTime(2014, 7, 12, 12, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)14193, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2014_205()
        {
            DateTime testTime = new DateTime(2014, 7, 24, 13, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)14205, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2015_1635()
        {
            DateTime testTime = new DateTime(2015, 6, 12, 14, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)15163, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2015_175()
        {
            DateTime testTime = new DateTime(2015, 6, 24, 15, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)15175, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2016_256()
        {
            DateTime testTime = new DateTime(2016, 9, 12, 16, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)16256, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2016_268()
        {
            DateTime testTime = new DateTime(2016, 9, 24, 17, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)16268, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2017_285()
        {
            DateTime testTime = new DateTime(2017, 10, 12, 18, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)17285, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2017_297()
        {
            DateTime testTime = new DateTime(2017, 10, 24, 19, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)17297, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2018_316()
        {
            DateTime testTime = new DateTime(2018, 11, 12, 20, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)18316, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2018_328()
        {
            DateTime testTime = new DateTime(2018, 11, 24, 21, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)18328, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2019_346()
        {
            DateTime testTime = new DateTime(2019, 12, 12, 22, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)19346, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2019_358()
        {
            DateTime testTime = new DateTime(2019, 12, 24, 23, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)19358, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2049_346()
        {
            DateTime testTime = new DateTime(2049, 12, 12, 22, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)49346, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2050_024()
        {
            DateTime testTime = new DateTime(2050, 1, 24, 23, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)24, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2069_358()
        {
            DateTime testTime = new DateTime(2069, 12, 24, 23, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)19358, buildNumber.Value);
        }

        [TestMethod]
        public void GetBuildNumberTest_2111_071()
        {
            DateTime testTime = new DateTime(2111, 3, 12, 4, 20, 30);
            ushort? buildNumber = (new BuildStamperUtility()).GetBuildNumber(testTime);

            Assert.IsNotNull(buildNumber);
            Assert.AreEqual((ushort)11071, buildNumber.Value);
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
