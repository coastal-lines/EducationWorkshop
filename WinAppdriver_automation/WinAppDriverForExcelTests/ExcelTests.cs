using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace WinAppDriverForExcelTests
{
    [TestClass]
    public class ExcelTests
    {
        static WindowsDriver<WindowsElement> session;

        [TestMethod]
        public void OpenExcelWithoutSplashScreen()
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Excel");
            appiumOptions.AddAdditionalCapability("appArguments", "/e");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        }

        [TestMethod]
        public void OpenExcelFileWithoutSplashScreen()
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Excel");
            appiumOptions.AddAdditionalCapability("appArguments", @"/e c:\Work2\Projects\GIT\EducationWorkshop\WinAppdriver_automation\WinAppDriverForExcelTests\Data\TaskReport.csv");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        }
    }
}
