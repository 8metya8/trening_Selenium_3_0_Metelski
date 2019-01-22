using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Firefox;

namespace Lesson_5
{
    //Сценарий, который проверяет, что при клике на товар открывается правильная страница товара в учебном приложении litecart.

     [TestFixture(typeof(ChromeDriver))]
     [TestFixture(typeof(FirefoxDriver))]
     [TestFixture(typeof(EdgeDriver))]
   
    class Task_10<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private TWebDriver driver;
        private WebDriverWait wait;

        [Test]
        public void TestTask_10_1()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/en/rubber-ducks-c-1/");

            IList<IWebElement> allDucks = driver.FindElements(By.CssSelector(".product"));
            string link;
            IWebElement element;

            for (int i = 0; i < allDucks.Count; i++)
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
            driver = new TWebDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
