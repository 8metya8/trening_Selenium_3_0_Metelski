using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace Lesson_5
{
    [TestFixture]

    class Task_10_2
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Test]
        public void TestTask_10_2()
        {

            driver.Navigate().GoToUrl("http://localhost/litecart/");
            IWebElement duck = driver.FindElement(By.CssSelector("#box-campaigns .product"));

            
        }

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
