using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Service;

namespace AppiumWADCourse
{
    public class AppiumAndWAD
    {
        public void OpenNotepadAndTakeAscreenshot()
        {
            /*
            //works with Appium 3.0.0.2
            //start WAD before

            WindowsDriver<WindowsElement> notePadSession;

            //DesiredCapabilities - is obsolete. In new packages it calls AppiumOptions class
            //Available capabilities: https://github.com/microsoft/WinAppDriver/blob/master/Docs/AuthoringTestScripts.md 
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", @"C:\Windows\System32\notepad.exe");


            notePadSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appCapabilities);

            if(notePadSession == null)
            {
                Console.WriteLine("App was not started");
                return;
            }

            Console.WriteLine($"Application title: {notePadSession.Title}");

            notePadSession.Manage().Window.Maximize();

            var screenShot = notePadSession.GetScreenshot();
            screenShot.SaveAsFile($".\\Screenshot{DateTime.Now.ToString("ddMMyyyyyhhmmss")}.png", System.Drawing.Imaging.ImageFormat.Jpeg);

            notePadSession.Quit();
            */
        }

        public void StartWithoutWADByAppiumServiceBuilder()
        {
            /*
            //works with Appium 3.0.0.2
            //solution works with Appium 3.0.0.2 and WAD 1.2.1.0

            var appiumLocalService = new AppiumServiceBuilder().UsingPort(4723).Build();
            appiumLocalService.Start();

            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");

            var session = new WindowsDriver<WindowsElement>(appiumLocalService, appCapabilities);
            */
        }

        public void SaveUserLogsIntoFile()
        {
            /*
            //works with Appium 3.0.0.2
            //solution works with Appium 3.0.0.2 and WAD 1.2.1.0

            var appiumLocalService = new AppiumServiceBuilder().UsingPort(4723).WithLogFile(new System.IO.FileInfo(@"TestLog.txt")).Build();
            appiumLocalService.Start();

            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");

            var session = new WindowsDriver<WindowsElement>(appiumLocalService, appCapabilities);
            */
        }

        public void UseLocatorsForSearchElements()
        {
            //solution works with Appium 4.1.1 and WAD 1.2.1.0
            WindowsDriver<WindowsElement> session;

            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");

            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
            var button = session.FindElementByName("Add new timer");
            button.Click();
        }

        public void FindTextAreaAndSendText()
        {
            //solution works with Appium 4.1.1 and WAD 1.2.1.0
            //FindElementByAccessibilityId equals @AutomationID in UI recorder
            WindowsDriver<WindowsElement> session;

            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");

            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
            var textArea = session.FindElementByAccessibilityId("CalculatorResults");
            textArea.SendKeys("12345");
            var text = textArea.Text;
        }

        public void ImplicitWaitForWAD()
        {
            //solution works with Appium 4.1.1 and WAD 1.2.1.0
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            WindowsDriver<WindowsElement> session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
            session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //global waiting
        }
    }
}
