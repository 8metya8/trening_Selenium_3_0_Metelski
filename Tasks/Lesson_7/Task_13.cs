using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace Lesson_7
{
    [TestFixture]
    public class Task_13
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
        public void TestTask_13()
        {
            for(int i = 0; i < 3; i++)
            {
                driver.Navigate().GoToUrl("http://localhost/litecart");
                //Click first product
                driver.FindElement(By.CssSelector("li.product")).Click();
                //Wait until open product page
                wait.Until(ExpectedConditions.ElementIsVisible(By.Name("add_cart_product")));
                //If product has option size, choose "Medium"
                if (driver.FindElements(By.CssSelector("[name=buy_now_form] td")).Count == 2)
                {
                    SelectElement select = new SelectElement(driver.FindElement(By.XPath("//*[@name='options[Size]']")));
                    select.SelectByValue("Medium");
                }
                //Add product to cart
                driver.FindElement(By.Name("add_cart_product")).Click();
                //Wait until cart update
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.CssSelector("span.quantity")), (i+ 1).ToString()));
            }

            //Go to Cart
            driver.FindElement(By.CssSelector("#cart .link")).Click();
            //Wait until cart will be open
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("[name=cart_form]")));

            //Get count how much kind of duck in the cart.
            int countOfItems = driver.FindElements(By.CssSelector("[name=cart_form]")).Count;
            string nameOfItem;
            IWebElement itemInOrder;

            //Delete all item from cart and wait until order update after delete.
            for(int i = 0; i < countOfItems; i++)
            {
                nameOfItem = driver.FindElement(By.CssSelector("[name=cart_form] strong")).Text;
                itemInOrder = driver.FindElement(By.XPath($"//td[text()='{nameOfItem}']" ));
                driver.FindElement(By.CssSelector("[name=remove_cart_item]")).Click();
                wait.Until(ExpectedConditions.StalenessOf(itemInOrder));
            }

            //Check that shows message that cart is empty.
            Assert.AreEqual(driver.FindElement(By.CssSelector("#content em")).Text, "There are no items in your cart.");
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
