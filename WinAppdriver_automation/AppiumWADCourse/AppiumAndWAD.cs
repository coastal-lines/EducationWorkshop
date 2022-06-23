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
        }

        public void StartWithoutWADByAppiumServiceBuilder()
        {
            var appiumLocalService = new AppiumServiceBuilder().UsingPort(4723).Build();
            appiumLocalService.Start();

            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");

            var session = new WindowsDriver<WindowsElement>(appiumLocalService, appCapabilities);
        }
    }
}
