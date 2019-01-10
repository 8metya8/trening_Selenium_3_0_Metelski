using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace Task_7
{

    [TestFixture]

    public class Task_7
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Test]
        public void TestTask7()
        {
            IList<IWebElement> menu = new List<IWebElement>();
            IList<IWebElement> subMenu = new List<IWebElement>();
            IList<IWebElement> elements;
            string text;

            HelperLogin.LogInAdmin(driver);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElement(By.CssSelector("li.selected")));
            Assert.IsTrue(driver.FindElements(By.CssSelector("td#content>h1")).Count == 1);
/*
            menu = driver.FindElements(By.CssSelector("ul#box-apps-menu>li"));

            foreach (IWebElement itemOfMenu in menu)
            {
                itemOfMenu.Click();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(By.CssSelector("li.selected")));

                Assert.IsTrue(driver.FindElements(By.CssSelector("td#content>h1")).Count == 1);

                menu = driver.FindElements(By.CssSelector("ul#box-apps-menu>li"));
                
                                subMenu = driver.FindElements(By.CssSelector("li.selected li"));

                                if(subMenu != null)
                                {
                                    foreach(IWebElement itemOfSubMenu in subMenu)
                                    {
                                        itemOfMenu.Click();

                                        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                                        wait.Until(d => d.FindElement(By.CssSelector("li.selected")));

                                        Assert.IsTrue(driver.FindElements(By.CssSelector("td#content>h1")).Count == 1);
                                    }
                                }
            }*/
            
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
