using System;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace Lesson_10
{
    [TestFixture]
    public class Task_17
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
                     
            driver = new ChromeDriver(options);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(25);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Manage().Window.Maximize();
        }

        [Test]
        public void TestTask_17()
        {
            IList<IWebElement> listOfEditLinks;

            HelperLogin.LogInAdmin(driver);

            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");
            listOfEditLinks = driver.FindElements(By.CssSelector("[title=Edit]"));

            for(int i = 2; i < listOfEditLinks.Count; i++)
            {
                listOfEditLinks[i].Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.Name("save")));

                //Check that console hasn't message of Severe Type (Error and otjer crirical messages)
                foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
                {
                    Assert.False(l.Level.Equals(LogLevel.Severe));
                }

                //Go back to catalog
                driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");
                //update list after reload page
                listOfEditLinks = driver.FindElements(By.CssSelector("[title=Edit]"));
            }
            
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
