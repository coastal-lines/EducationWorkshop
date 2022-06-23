using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace MSTestOverview
{
    [TestClass]
    public class AlarmsAndClockSmokeTest
    {
        [TestMethod]
        public void TestAlarmsAndClockIsLaunchingSuccesssfully()
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");
            WindowsDriver<WindowsElement> session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
            session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //global waiting

            Assert.AreEqual("Alarms & Clock", session.Title.ToString(), $"Wrong title of WindowsAlarms: should be {"Alarms & Clock"} but was {session.Title.ToString()}");

            session.Quit();
        }
    }
}
