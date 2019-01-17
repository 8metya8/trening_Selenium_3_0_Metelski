using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace Lesson_5
{
    [TestFixture]

    class Task_10
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Test]
        public void TestTask_10()
        {

            driver.Navigate().GoToUrl("http://localhost/litecart/en/rubber-ducks-c-1/");

            IList<IWebElement> allDucks = driver.FindElements(By.CssSelector(".product"));
            string link;
            IWebElement element;

            for(int i = 0; i < allDucks.Count; i++)
            {
                element = allDucks[i].FindElement(By.CssSelector(".link"));
                link = element.GetAttribute("href");
                element.Click();

                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[name=add_cart_product]")));

                Assert.IsTrue(driver.Url.Equals(link));

                driver.Navigate().GoToUrl("http://localhost/litecart/en/rubber-ducks-c-1/");
                allDucks = driver.FindElements(By.CssSelector(".product"));
            }
        }

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

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
