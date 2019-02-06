using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using OpenQA.Selenium.IE;

namespace Test_remote
{
    [TestFixture]
    public class UnitTest1
    {
        IWebDriver driver;

        [Test]
        public void TestMethod1()
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability(CapabilityType.BrowserName, "firefox");

            driver = new RemoteWebDriver(new Uri("http://192.168.56.1:4444/wd/hub"), capabilities);
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("http://barancev.github.io/how-to-set-datepicker-value/");
            driver.Quit();
        }
    }
}
