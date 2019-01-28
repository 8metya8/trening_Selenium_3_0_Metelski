using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace Lesson_6
{
    [TestFixture]
    public class Task_12
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
        public void TestTask_12()
        {
            string randomTestIndex = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString();

            HelperLogin.LogInAdmin(driver);
            
            //Catalog
            driver.FindElement(By.XPath("//a[contains(@href, 'app=catalog')]")).Click();
            
            //Wait and Click button 'Add New Product'
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(), ' Add New Product')]"))).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='name[en]']")));
            
            //Fill tab General
            driver.FindElement(By.XPath("//input[@name='status']")).Click();
            
            //Name
            driver.FindElement(By.XPath("//input[@name='name[en]']")).SendKeys("Name" + randomTestIndex);
            
            //Code
            driver.FindElement(By.XPath("//input[@name='code']")).SendKeys(randomTestIndex.Substring(randomTestIndex.Length - 5, 5));
            
            //Gender - select all
            foreach(IWebElement element in driver.FindElements(By.XPath("//input[@name='product_groups[]']")))
            {
                element.Click();
            }

            //Quantity
            driver.FindElement(By.Name("quantity")).Clear();
            driver.FindElement(By.Name("quantity")).SendKeys(new Random().Next(5, 15).ToString());

            //File
            string pathToFile = "icon.jpg";            
            driver.FindElement(By.XPath("//*[@name='new_images[]']")).SendKeys(Path.GetFullPath(pathToFile));

            //Date Valid From
            SetDatepicker(driver, "[name=date_valid_from]", DateTime.Now.Date.ToShortDateString());
            //Date Valid To
            SetDatepicker(driver, "[name=date_valid_to]", DateTime.Now.AddDays(10).Date.ToShortDateString());

            //Go to tab "Information"
            driver.FindElement(By.XPath("//a[text() = 'Information']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[name=manufacturer_id]")));

            //Select Manufaturer
            SelectElement select = new SelectElement(driver.FindElement(By.CssSelector("[name=manufacturer_id]")));
            select.SelectByValue("1");

            //Enter keywords
            driver.FindElement(By.CssSelector("[name=keywords]")).SendKeys("Keyword" + randomTestIndex);

            //Enter Short Description
            driver.FindElement(By.XPath("//*[@name='short_description[en]']")).SendKeys("Short Decription" + randomTestIndex);

            //Enter Description
            driver.FindElement(By.CssSelector(".trumbowyg-editor")).SendKeys("Decription" + randomTestIndex);

            //Enter Head Title
            driver.FindElement(By.XPath("//*[@name='head_title[en]']")).SendKeys("Head Title" + randomTestIndex);

            //Enter Meta Description
            driver.FindElement(By.XPath("//*[@name='meta_description[en]']")).SendKeys("Meta Description" + randomTestIndex);

            //Go to tab Price
            driver.FindElement(By.XPath("//a[text() = 'Prices']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[name=purchase_price_currency_code]")));

            //Select Purchase price currency code
            select = new SelectElement(driver.FindElement(By.CssSelector("[name=purchase_price_currency_code]")));
            select.SelectByValue("USD");

            //Enter Purchase Prise
            driver.FindElement(By.CssSelector("[name=purchase_price]")).Clear();
            driver.FindElement(By.CssSelector("[name=purchase_price]")).SendKeys(randomTestIndex.Substring(randomTestIndex.Length - 2, 2));

            //Enter Gross Price
            driver.FindElement(By.XPath("//*[@name='gross_prices[USD]']")).Clear();
            driver.FindElement(By.XPath("//*[@name='gross_prices[USD]']")).SendKeys(randomTestIndex.Substring(randomTestIndex.Length - 2, 2));
            driver.FindElement(By.XPath("//*[@name='gross_prices[EUR]']")).Clear();
            driver.FindElement(By.XPath("//*[@name='gross_prices[EUR]']")).SendKeys(randomTestIndex.Substring(randomTestIndex.Length - 2, 2));

            //Save
            driver.FindElement(By.CssSelector("[name=save]")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(), ' Add New Product')]")));

            //Check that Catalog contains product with new name
            Assert.IsTrue(driver.FindElements(By.XPath("//a[text()='Name" + randomTestIndex + "']")).Count == 1);
        }

        public static void SetDatepicker(IWebDriver driver, string cssSelector, string date)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelector)));

            driver.FindElement(By.CssSelector(cssSelector)).SendKeys(date);
            //Click to field Code for close calendar
            driver.FindElement(By.XPath("//input[@name='code']")).Click();
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
