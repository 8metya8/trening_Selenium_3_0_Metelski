using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    class Task_8
    {
        private IWebDriver driver;
        private WebDriverWait wait;
   
        [Test]
        public void TestTask8()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/");

            IList<IWebElement> allDucks = driver.FindElements(By.CssSelector("ul.listing-wrapper>li"));

            foreach(IWebElement duck in allDucks)
            {
                Assert.IsTrue(duck.FindElements(By.CssSelector("div.sticker")).Count == 1);
            }         

        }

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
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
