using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace Tasks
{

    [TestFixture]

    public class Task_7
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Test]
        public void TestTask7()
        {
            IList<IWebElement> menu;
            IList<IWebElement> subMenu;

            HelperLogin.LogInAdmin(driver);

            menu = driver.FindElements(By.CssSelector("ul#box-apps-menu>li"));

            for (int a = 0; a < menu.Count; a++)
            {
                menu[a].Click();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(By.CssSelector("li.selected")));
                menu = driver.FindElements(By.CssSelector("ul#box-apps-menu>li"));

                Assert.IsTrue(driver.FindElements(By.CssSelector("td#content>h1")).Count == 1);

                subMenu = driver.FindElements(By.CssSelector("li.selected li"));

                                if(subMenu != null)
                                {
                                    for (int i = 1; i < subMenu.Count; i++)
                                     {
                                        subMenu[i].Click();

                                        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                                        wait.Until(d => d.FindElement(By.CssSelector("li.selected")));

                                        Assert.IsTrue(driver.FindElements(By.CssSelector("td#content>h1")).Count == 1);

                                        subMenu = driver.FindElements(By.CssSelector("li.selected li"));
                                      }
                                }


                menu = driver.FindElements(By.CssSelector("ul#box-apps-menu>li"));
            }
            
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
