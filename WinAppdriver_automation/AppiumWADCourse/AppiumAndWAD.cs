using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Service;
using System.Diagnostics;
using OpenQA.Selenium;

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

        public void LaunchClock()
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");
            WindowsDriver<WindowsElement> session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        }

        public void ConnectToAlreadyOpenedApplication()
        {
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Root");

            WindowsDriver<WindowsElement> session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);

            AppiumOptions windowOptions = null;
            WindowsDriver<WindowsElement> sessionSecondBest = null;

            var listOfAllWindows = session.FindElementsByXPath(@"//Window");

            Debug.WriteLine($"Elements found: {listOfAllWindows.Count}");


            foreach (var window in listOfAllWindows)
            {
                Console.WriteLine($"{window.Text}");

                if (window.Displayed && window.Text.Contains("Calculator"))
                {
                    var windowHandle = window.GetAttribute("NativeWindowHandle");
                    Console.WriteLine($"Window Handle: {windowHandle}");

                    var handleInt = (int.Parse(windowHandle)).ToString("x");
                    windowOptions = new AppiumOptions();
                    windowOptions.AddAdditionalCapability("appTopLevelWindow", handleInt);
                    break;
                }
            }

            if (windowOptions != null)
            {
                sessionSecondBest = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), windowOptions);
                sessionSecondBest.Keyboard.SendKeys(Keys.Escape);
                sessionSecondBest.Keyboard.SendKeys("2*2" + Keys.Enter);
            }

            /*
             https://github.com/microsoft/WinAppDriver/wiki/Frequently-Asked-Questions#what-to-do-when-an-application-has-a-splash-screen-or-winappdriver-fails-to-recognize-the-correct-window
            When and how to attach to an existing App Window
            In some cases, you may want to test applications that are not launched in a conventional way like
            shown above. For instance, the Cortana application is always running and will not launch a
            UI window until triggered through Start Menu or a keyboard shortcut. In this case, you can create a
            new session in Windows Application Driver by providing the application top level window handle as
            a hex string (E.g. 0xB822E2). This window handle can be retrieved from various methods including 
            the Desktop Session mentioned above. This mechanism can also be used for applications that have
            unusually long startup times. Below is an example of creating a test session for the Cortana app
            after launching the UI using a keyboard shortcut and locating the window using the Desktop Session.
             */
        }
    }
}
