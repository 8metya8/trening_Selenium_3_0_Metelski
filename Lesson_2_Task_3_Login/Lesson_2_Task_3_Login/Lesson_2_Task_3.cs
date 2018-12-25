using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Lesson_2_Task_3_Login
{
    [TestFixture]
    
    public class Lesson_2_Task_3
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            driver.Manage().Window.Maximize();
        }

        [Test]
        public void TestLogin()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/login.php");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            wait.Until(ExpectedConditions.UrlMatches("http://localhost/litecart/admin/"));
            driver.FindElement(By.XPath("//ul[@id='box-apps-menu']/li[1]")).Click();
            wait.Until(ExpectedConditions.TitleContains("Template"));
            driver.FindElement(By.XPath("//a[@title='Logout']")).Click();
            wait.Until(ExpectedConditions.UrlMatches("http://localhost/litecart/admin/login.php"));
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
