using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildStamper;
using System.IO;

namespace BuildStamperTest
{
    /// <summary>
    /// Summary description for BuildStamperUtilityTests
    /// </summary>
    [TestClass]
    public class BuildStamperFileHandlingTests
    {
        public BuildStamperFileHandlingTests()
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
        public void StampAssemblyInfoFileStampOnlyTest()
        {
            string testFilename = "..\\..\\..\\TestFiles\\AssemblyInfoProject\\TestAssemblyInfo.cs";
            FileInfo fileInfo = new FileInfo(testFilename);
            Assert.IsTrue(fileInfo.Exists);

            BuildStamperUtility uut = new BuildStamperUtility();

            bool wasProcessed = uut.StampAssemblyInfoFile(fileInfo.FullName, null);
            System.Diagnostics.Debug.WriteLine(String.Format("WasProcessed: {0}", wasProcessed.ToString()));
        }

        [TestMethod]
        public void StampAssemblyInfoFileWithVersionNumberTest()
        {
            string testFilename = "..\\..\\..\\TestFiles\\AssemblyInfoProject\\TestAssemblyInfo.cs";
            FileInfo fileInfo = new FileInfo(testFilename);
            Assert.IsTrue(fileInfo.Exists);

            BuildStamperUtility uut = new BuildStamperUtility();

            VersionDefinition newVersion = new VersionDefinition(0, 9, 11);
            bool wasProcessed = uut.StampAssemblyInfoFile(fileInfo.FullName, newVersion);
            System.Diagnostics.Debug.WriteLine(String.Format("WasProcessed: {0}", wasProcessed.ToString()));
        }

        [TestMethod]
        public void EnumerateDirectoriesStampOnlyTest()
        {
            string testDirectory = "..\\..\\..\\TestFiles";
            DirectoryInfo dirInfo = new DirectoryInfo(testDirectory);
            Assert.IsTrue(dirInfo.Exists);

            BuildStamperUtility uut = new BuildStamperUtility();

            StringWriter consoleOutput = new StringWriter();
            TextWriter originalConsoleOutput = Console.Out;
            Console.SetOut(consoleOutput);

            uut.EnumerateDirectories(dirInfo.FullName, null);

            Console.SetOut(originalConsoleOutput);
            System.Diagnostics.Debug.WriteLine(consoleOutput.ToString());
        }

        [TestMethod]
        public void EnumerateDirectoriesWithVersionTest()
        {
            string testDirectory = "..\\..\\..\\TestFiles";
            DirectoryInfo dirInfo = new DirectoryInfo(testDirectory);
            Assert.IsTrue(dirInfo.Exists);

            BuildStamperUtility uut = new BuildStamperUtility();

            StringWriter consoleOutput = new StringWriter();
            TextWriter originalConsoleOutput = Console.Out;
            Console.SetOut(consoleOutput);

            VersionDefinition newVersion = new VersionDefinition(0, 9, 15);
            uut.EnumerateDirectories(dirInfo.FullName, newVersion);

            Console.SetOut(originalConsoleOutput);
            System.Diagnostics.Debug.WriteLine(consoleOutput.ToString());
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
