using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace WinFormsTests
{
    [TestClass]
    public class WinFormsTests
    {
        static WindowsDriver<WindowsElement> sessionWinForm;

        [ClassInitialize]
        public static void SetupDriver()
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            //appiumOptions.AddAdditionalCapability("app", @"C:\Work2\Projects\GIT\EducationWorkshop\WinAppdriver_automation\WinFormsTests\Data\Application+under+test\DoNotDistrurbMortgageCalculatorFrom1999.exe");
            appiumOptions.AddAdditionalCapability("app", WinFormsTestsProject.Properties.Settings.Default.ApplicationPath);
            sessionWinForm = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        }

        [TestMethod]
        public void CheckBoxTest()
        {
            
        }
    }
}
