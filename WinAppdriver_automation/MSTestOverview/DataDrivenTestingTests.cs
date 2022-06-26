using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MSTestOverview
{
    [TestClass]
    class DataDrivenTestingTests
    {
        static WindowsDriver<WindowsElement> session;
        private static TestContext objTestContext;

        /*
        [ClassInitialize]
        public static void PrepareForTestingAlarms(TestContext testContext)
        {
            Debug.WriteLine("ClassInitialize -> PrepareForTestingAlarms");
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);

            objTestContext = testContext;
        }*/

        //[DataSource("System.Data.OleDb", @"Data Source=c:\Work2\Projects\GIT\EducationWorkshop\WinAppdriver_automation\MSTestOverview\Data\Data.xlsx")]
        [TestMethod]
        public void TestBasedOnDDT()
        {
            // Click the clock button in top section of the app
            session.FindElementByAccessibilityId("ClockButton").Click();

            session.FindElementByName("Add new clock").Click();

            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(10));

            var txtLocation = session.FindElementByName("Enter a location");

            waitForMe.Until(pred => txtLocation.Displayed);

            string locInput = Convert.ToString(objTestContext.DataRow["Location Input"]);

            Debug.WriteLine($"Input from Excel: {locInput}");

            txtLocation.SendKeys(locInput); //"Lahore, Pakistan");
            txtLocation.SendKeys(Keys.Enter);

            var clockItems = session.FindElementsByAccessibilityId("WorldClockItemGrid");

            bool wasClockTileFound = false;
            WindowsElement tileFound = null;

            string strExpectedValue = Convert.ToString(objTestContext.DataRow["Location Expected"]);
            Debug.WriteLine($"Expected value: {strExpectedValue}");

            foreach (WindowsElement clockTile in clockItems)
            {
                if (clockTile.Text.StartsWith(strExpectedValue))//"Lahore, Pakistan"))
                {
                    wasClockTileFound = true;
                    tileFound = clockTile;
                    Debug.WriteLine("Clock found.");
                    break;
                }
            }

            Assert.IsTrue(wasClockTileFound, "No clock tile found.");

            waitForMe.Until(pred => tileFound.Displayed);

            Actions actionForRightClick = new Actions(session);
            actionForRightClick.MoveToElement(tileFound);
            actionForRightClick.Click();
            actionForRightClick.ContextClick(tileFound);
            actionForRightClick.Perform();
        }
    }
}
