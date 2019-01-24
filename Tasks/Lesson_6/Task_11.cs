using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Lesson_6
{
    [TestFixture]
    public class Task_11
    {
        IWebDriver driver;
        WebDriverWait wait;

        [Test]
        public void TestTask_11()
        {
            string randomTestIndex = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString();
            string email = "email" + randomTestIndex + "@gmail.com";

            driver.Navigate().GoToUrl("http://localhost/litecart/en/create_account");
            //FirstName
            driver.FindElement(By.Name("firstname")).SendKeys("Name" + randomTestIndex);
            //LastName
            driver.FindElement(By.Name("lastname")).SendKeys("Last" + randomTestIndex);
            //Address1
            driver.FindElement(By.Name("address1")).SendKeys("Address " + randomTestIndex);
            //Posrcode
            driver.FindElement(By.Name("postcode")).SendKeys(randomTestIndex.Substring(randomTestIndex.Length-5, 5));
            //City
            driver.FindElement(By.Name("city")).SendKeys("Washington");
            //Select country
            SelectElement selectCountry = new SelectElement(driver.FindElement(By.Name("country_code")));
            selectCountry.SelectByText("United States");
            //select Zone 
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("select[name=zone_code]")));
            SelectElement selectState = new SelectElement(driver.FindElement(By.CssSelector("select[name=zone_code]")));
            //Select random state
            selectState.SelectByValue(selectState.Options[new Random().Next(0, selectState.Options.Count - 1)].GetAttribute("value"));
            //Email
            driver.FindElement(By.Name("email")).SendKeys(email);
            //Phone
            driver.FindElement(By.Name("phone")).SendKeys("+1" + randomTestIndex);
            //Passsword
            driver.FindElement(By.Name("password")).SendKeys(randomTestIndex);
            //Confirm password
            driver.FindElement(By.Name("confirmed_password")).SendKeys(randomTestIndex);
            //Button Registration
            driver.FindElement(By.Name("create_account")).Click();

            //Logout
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[contains(text(), 'Logout')]")));           
            driver.FindElement(By.XPath("//*[contains(text(), 'Logout')]")).Click();

            //Login
            wait.Until(ExpectedConditions.ElementExists((By.Name("login"))));
            //Email
            driver.FindElement(By.Name("email")).SendKeys(email);
            //Password
            driver.FindElement(By.Name("password")).SendKeys(randomTestIndex);
            //Login
            driver.FindElement(By.Name("login")).Click();

            //Logout
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[contains(text(), 'Logout')]")));
            driver.FindElement(By.XPath("//*[contains(text(), 'Logout')]")).Click();
            wait.Until(ExpectedConditions.ElementExists((By.Name("login"))));
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
