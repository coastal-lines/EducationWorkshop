using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace MSTestOverview
{
    [TestClass]
    public class AlarmsAndClockSmokeTest
    {
        static WindowsDriver<WindowsElement> session;

        [ClassInitialize]
        public static void PrepareForTestingAlarms(TestContext testContext)
        {
            Debug.WriteLine("ClassInitialize -> PrepareForTestingAlarms");
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
            session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //global waiting
        }

        [ClassCleanup]
        public static void CleanUpAfterTests()
        {
            Debug.WriteLine("ClassInitialize -> CleanUpAfterTests");
            session.Quit();
        }

        [TestInitialize]
        public void BeforeTest()
        {
            Debug.WriteLine("Before any test");
        }

        [TestCleanup]
        public void AfterTest()
        {
            Debug.WriteLine("After any test");
        }

        [TestMethod]
        public void TestAlarmsAndClockIsLaunchingSuccesssfully()
        {
            Assert.AreEqual("Alarms & Clock", session.Title.ToString(), $"Wrong title of WindowsAlarms: should be {"Alarms & Clock"} but was {session.Title.ToString()}");
        }
    }
}
