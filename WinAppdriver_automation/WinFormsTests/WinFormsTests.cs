using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;

namespace WinFormsTests
{
    [TestClass]
    public class WinFormsTests
    {
        static WindowsDriver<WindowsElement> sessionWinForm;

        [ClassInitialize]
        public static void SetupDriver(TestContext testContext)
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            //appiumOptions.AddAdditionalCapability("app", @"C:\Work2\Projects\GIT\EducationWorkshop\WinAppdriver_automation\WinFormsTests\Data\Application+under+test\DoNotDistrurbMortgageCalculatorFrom1999.exe");
            appiumOptions.AddAdditionalCapability("app", WinFormsTestsProject.Properties.Settings.Default.ApplicationPath);
            sessionWinForm = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        }

        [TestMethod]
        public void CheckBoxTest()
        {
            var check = sessionWinForm.FindElementByName("checkBox1");
            check.Click();

            System.Threading.Thread.Sleep(1000);
        }

        [TestMethod]
        public void ComboTest()
        {
            var combo = sessionWinForm.FindElementByAccessibilityId("comboBox1");
            var open = combo.FindElementByName("Open");
            combo.SendKeys(Keys.Down);
            open.Click();

            //Element "/ListITem//" is a tag
            var listItems = combo.FindElementsByTagName("ListItem");
            Assert.AreEqual(6, listItems.Count, "Wrong number of list items");

            //Waiting amount of items
            WebDriverWait wait = new WebDriverWait(sessionWinForm, TimeSpan.FromSeconds(10));
            foreach (var comboKid in listItems)
            {
                if(comboKid.Text == "NJ")
                {
                    wait.Until(x => comboKid.Displayed);
                    comboKid.Click();
                }
            }
        }
    }
}
