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
    public class VersionDefinitionTests
    {
        public VersionDefinitionTests()
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
        public void CompareToTest()
        {
            VersionDefinition testVersion = new VersionDefinition(2, 3, 4, 5);
            VersionDefinition compareVersion = new VersionDefinition(2, 3, 4, 5);

            Assert.AreEqual(0, testVersion.CompareTo(compareVersion));

            compareVersion.Build = null;
            Assert.AreEqual(1, testVersion.CompareTo(compareVersion));

            compareVersion = new VersionDefinition(2, 3, 4, 5);
            compareVersion.Revision = null;
            Assert.AreEqual(1, testVersion.CompareTo(compareVersion));

            compareVersion = new VersionDefinition(2, 3, 4, 5);
            compareVersion.Minor = null;
            Assert.AreEqual(1, testVersion.CompareTo(compareVersion));

            compareVersion = new VersionDefinition(2, 3, 4, 5);
            compareVersion.Major = null;
            Assert.AreEqual(1, testVersion.CompareTo(compareVersion));

            Assert.AreEqual(1, testVersion.CompareTo(null));
            Assert.AreEqual(1, testVersion.CompareTo(""));

            compareVersion = new VersionDefinition(1, 3, 4, 5);
            Assert.AreEqual(1, testVersion.CompareTo(compareVersion));

            compareVersion = new VersionDefinition(2, 2, 4, 5);
            Assert.AreEqual(1, testVersion.CompareTo(compareVersion));

            compareVersion = new VersionDefinition(2, 3, 3, 5);
            Assert.AreEqual(1, testVersion.CompareTo(compareVersion));

            compareVersion = new VersionDefinition(2, 3, 4, 4);
            Assert.AreEqual(1, testVersion.CompareTo(compareVersion));

            compareVersion = new VersionDefinition(3, 3, 4, 5);
            Assert.AreEqual(-1, testVersion.CompareTo(compareVersion));

            compareVersion = new VersionDefinition(2, 4, 4, 5);
            Assert.AreEqual(-1, testVersion.CompareTo(compareVersion));

            compareVersion = new VersionDefinition(2, 3, 5, 5);
            Assert.AreEqual(-1, testVersion.CompareTo(compareVersion));

            compareVersion = new VersionDefinition(2, 3, 4, 6);
            Assert.AreEqual(-1, testVersion.CompareTo(compareVersion));
        }

        [TestMethod]
        public void EqualsOperatorTest()
        {
            VersionDefinition testVersion = new VersionDefinition(2, 3, 4, 5);
            VersionDefinition compareVersion = new VersionDefinition(2, 3, 4, 5);

            bool result = (testVersion == compareVersion);
            Assert.IsTrue(result);

            compareVersion.Build = null;
            result = (testVersion == compareVersion);
            Assert.IsFalse(result);

            compareVersion = new VersionDefinition(2, 3, 4, 5);
            compareVersion.Revision = null;
            result = (testVersion == compareVersion);
            Assert.IsFalse(result);

            compareVersion = new VersionDefinition(2, 3, 4, 5);
            compareVersion.Minor = null;
            result = (testVersion == compareVersion);
            Assert.IsFalse(result);

            compareVersion = new VersionDefinition(2, 3, 4, 5);
            compareVersion.Major = null;
            result = (testVersion == compareVersion);
            Assert.IsFalse(result);

            result = (testVersion == null);
            Assert.IsFalse(result);
            result = (null == testVersion);
            Assert.IsFalse(result);

            compareVersion = new VersionDefinition(1, 3, 4, 5);
            result = (testVersion == compareVersion);
            Assert.IsFalse(result);

            compareVersion = new VersionDefinition(2, 2, 4, 5);
            result = (testVersion == compareVersion);
            Assert.IsFalse(result);

            compareVersion = new VersionDefinition(2, 3, 3, 5);
            result = (testVersion == compareVersion);
            Assert.IsFalse(result);

            compareVersion = new VersionDefinition(2, 3, 4, 4);
            result = (testVersion == compareVersion);
            Assert.IsFalse(result);

            compareVersion = new VersionDefinition(3, 3, 4, 5);
            result = (testVersion == compareVersion);
            Assert.IsFalse(result);

            compareVersion = new VersionDefinition(2, 4, 4, 5);
            result = (testVersion == compareVersion);
            Assert.IsFalse(result);

            compareVersion = new VersionDefinition(2, 3, 5, 5);
            result = (testVersion == compareVersion);
            Assert.IsFalse(result);

            compareVersion = new VersionDefinition(2, 3, 4, 6);
            result = (testVersion == compareVersion);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NotEqualsOperatorTest()
        {
            VersionDefinition testVersion = new VersionDefinition(2, 3, 4, 5);
            VersionDefinition compareVersion = new VersionDefinition(2, 3, 4, 5);

            bool result = (testVersion != compareVersion);
            Assert.IsFalse(result);

            compareVersion.Build = null;
            result = (testVersion != compareVersion);
            Assert.IsTrue(result);

            compareVersion = new VersionDefinition(2, 3, 4, 5);
            compareVersion.Revision = null;
            result = (testVersion != compareVersion);
            Assert.IsTrue(result);

            compareVersion = new VersionDefinition(2, 3, 4, 5);
            compareVersion.Minor = null;
            result = (testVersion != compareVersion);
            Assert.IsTrue(result);

            compareVersion = new VersionDefinition(2, 3, 4, 5);
            compareVersion.Major = null;
            result = (testVersion != compareVersion);
            Assert.IsTrue(result);

            result = (testVersion != null);
            Assert.IsTrue(result);
            result = (null != testVersion);
            Assert.IsTrue(result);

            compareVersion = new VersionDefinition(1, 3, 4, 5);
            result = (testVersion != compareVersion);
            Assert.IsTrue(result);

            compareVersion = new VersionDefinition(2, 2, 4, 5);
            result = (testVersion != compareVersion);
            Assert.IsTrue(result);

            compareVersion = new VersionDefinition(2, 3, 3, 5);
            result = (testVersion != compareVersion);
            Assert.IsTrue(result);

            compareVersion = new VersionDefinition(2, 3, 4, 4);
            result = (testVersion != compareVersion);
            Assert.IsTrue(result);

            compareVersion = new VersionDefinition(3, 3, 4, 5);
            result = (testVersion != compareVersion);
            Assert.IsTrue(result);

            compareVersion = new VersionDefinition(2, 4, 4, 5);
            result = (testVersion != compareVersion);
            Assert.IsTrue(result);

            compareVersion = new VersionDefinition(2, 3, 5, 5);
            result = (testVersion != compareVersion);
            Assert.IsTrue(result);

            compareVersion = new VersionDefinition(2, 3, 4, 6);
            result = (testVersion != compareVersion);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FromStringTests()
        {
            VersionDefinition testVersion;

            testVersion = VersionDefinition.FromString("2.3.4.5");
            Assert.AreEqual(new VersionDefinition(2, 3, 4, 5), testVersion);

            testVersion = VersionDefinition.FromString("2.3.4");
            Assert.AreEqual(new VersionDefinition(2, 3, 4), testVersion);

            testVersion = VersionDefinition.FromString("2.3");
            Assert.AreEqual(new VersionDefinition(2, 3), testVersion);

            testVersion = VersionDefinition.FromString("2");
            Assert.IsNull(testVersion);

            testVersion = VersionDefinition.FromString("2.3.4.5.6");
            Assert.IsNull(testVersion);

            testVersion = VersionDefinition.FromString("");
            Assert.IsNull(testVersion);

            testVersion = VersionDefinition.FromString("Bogus");
            Assert.IsNull(testVersion);

            testVersion = VersionDefinition.FromString(null);
            Assert.IsNull(testVersion);
        }

        [TestMethod]
        public void ToStringTests()
        {
            VersionDefinition testVersion = new VersionDefinition(2, 3, 4, 5);

            Assert.AreEqual("2.3.4.5", testVersion.ToString());

            testVersion.Build = null;
            Assert.AreEqual("2.3.4", testVersion.ToString());

            testVersion.Revision = null;
            Assert.AreEqual("2.3", testVersion.ToString());

            testVersion.Minor = null;
            Assert.AreEqual("InvalidFormat", testVersion.ToString());

            testVersion = new VersionDefinition(2, 3, 4, 5);
            testVersion.Major = null;
            Assert.AreEqual("InvalidFormat", testVersion.ToString());

            testVersion = new VersionDefinition(2, 3, 4, 5);
            testVersion.Minor = null;
            Assert.AreEqual("InvalidFormat", testVersion.ToString());

            testVersion = new VersionDefinition(2, 3, 4, 5);
            testVersion.Revision = null;
            Assert.AreEqual("InvalidFormat", testVersion.ToString());
        }

        [TestMethod]
        public void CloneTest()
        {
            VersionDefinition uut = new VersionDefinition(1, 2, 3, 4);

            VersionDefinition clone = uut.Clone();

            Assert.IsNotNull(clone);
            Assert.AreEqual(uut.Major, clone.Major);
            Assert.AreEqual(uut.Minor, clone.Minor);
            Assert.AreEqual(uut.Revision, clone.Revision);
            Assert.AreEqual(uut.Build, clone.Build);

            Assert.AreEqual(uut.ToString(), clone.ToString());

            Assert.IsFalse(Object.ReferenceEquals(uut, clone));
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InvalidVersionNumberTest()
        {
            VersionDefinition uut = new VersionDefinition(65535, 0, 0, 0);
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
