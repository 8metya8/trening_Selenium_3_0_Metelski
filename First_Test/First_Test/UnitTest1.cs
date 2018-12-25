using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Task_1
{
    [TestFixture]
    public class UnitTest1
    {
        private IWebDriver driver;
        
        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();           
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void FirstTest()
        {
            driver.Navigate().GoToUrl("https://tut.by/");
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }

    }
}
