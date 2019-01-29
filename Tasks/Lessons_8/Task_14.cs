using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace Lesson_8
{
    [TestFixture]
    public class Task_14
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(25);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Manage().Window.Maximize();
        }

        [Test]
        public void TestTask_14()
        {
            HelperLogin.LogInAdmin(driver);
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");
            //Open Add New Country
            driver.FindElement(By.CssSelector(".button")).Click();
            //Wait open page 
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[name=save]")));

            //Get all external links
            IList<IWebElement> listOfLinks = driver.FindElements(By.CssSelector(".fa.fa-external-link"));
            string currentWindowId = driver.CurrentWindowHandle;

            foreach(IWebElement link in listOfLinks)
            {
                //Click to Link
                link.Click();
                //Wait until open new window
                wait.Until(driver => driver.WindowHandles.Count == 2);
                //Switch to new window and close it
                driver.SwitchTo().Window(GetNewWindowHandle(driver, currentWindowId));
                driver.Close();
                //Return to main window
                driver.SwitchTo().Window(currentWindowId);
            }
        }
        
        public static string GetNewWindowHandle(IWebDriver driver, string currentWindowId)
        {
            List<string> listOfWindowsHandle = new List<string>(driver.WindowHandles);
            listOfWindowsHandle.Remove(currentWindowId);

            return listOfWindowsHandle[0];
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
