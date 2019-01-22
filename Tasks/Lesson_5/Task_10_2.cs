using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

namespace Lesson_5
{
    /*
      Нужно открыть главную страницу, выбрать первый товар в блоке Campaigns и проверить следующее:

      а) на главной странице и на странице товара совпадает текст названия товара
      б) на главной странице и на странице товара совпадают цены (обычная и акционная)
      в) обычная цена зачёркнутая и серая (можно считать, что "серый" цвет это такой, у которого в RGBa представлении одинаковые значения для каналов R, G и B)
      г) акционная жирная и красная (можно считать, что "красный" цвет это такой, у которого в RGBa представлении каналы G и B имеют нулевые значения)
         (цвета надо проверить на каждой странице независимо, при этом цвета на разных страницах могут не совпадать)
      д) акционная цена крупнее, чем обычная (это тоже надо проверить на каждой странице независимо)
         
         
    */

    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(EdgeDriver))]

    class Task_10_2<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private TWebDriver driver;

        [Test]
        public void TestTask_10_2()
        {
            int r;
            int g;
            int b;

            driver.Navigate().GoToUrl("http://localhost/litecart/");
            
            //Get parametrs for duck on main page
            string nameOnMainPage = driver.FindElement(By.CssSelector("#box-campaigns .name")).Text;

            string costOnMainPage = driver.FindElement(By.CssSelector("#box-campaigns .regular-price")).Text;
            int hieghtCostOnMainPage = driver.FindElement(By.CssSelector("#box-campaigns .regular-price")).Size.Height;
            int widthCostOnMainPage = driver.FindElement(By.CssSelector("#box-campaigns .regular-price")).Size.Width;
            string colorCostOnMainPage = driver.FindElement(By.CssSelector("#box-campaigns .regular-price")).GetCssValue("color");
            string tagNameOfCost = driver.FindElement(By.CssSelector("#box-campaigns .regular-price")).TagName;

            string compaignCostOnMainPage = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price")).Text;
            int hieghtCompaignCostOnMainPage = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price")).Size.Height;
            int widthCompaignCostOnMainPage = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price")).Size.Width;
            string colorCompaignCostOnMainPage = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price")).GetCssValue("color");
            string tagNameOfCompaignCost = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price")).TagName;


            //Check color of main price and tag name
            string[] rgb = colorCostOnMainPage.Split(',');
            string colorModel = rgb[0].Split('(')[0];

            Int32.TryParse(rgb[0].Split('(')[1], out r);
            Int32.TryParse(rgb[1], out g);
            Int32.TryParse(rgb[2], out b);

            //Choose check depending on color model because in Chrome: grei color is R=G=B and RGB != 0.
            // in Edge, Firefox: grei color is R=G and B = 0.

            switch (colorModel)
            {

                case "rgba":
                    {
                        Assert.IsTrue((r == g) && (g == b));
                        break;
                    }
                case "rgb":
                    {
                        Assert.IsTrue((r == g) && (b == 0));
                        break;
                    }
            }          
            
            Assert.AreEqual(tagNameOfCost, "s");
            
            //Check color of compaign price and tag name
            rgb = colorCompaignCostOnMainPage.Split(',');

            Int32.TryParse(rgb[0].Split('(')[1], out r);
            Int32.TryParse(rgb[1], out g);
            Int32.TryParse(rgb[2], out b);

            Assert.IsTrue((r != 0) && (g == 0) && (b == 0));
            Assert.AreEqual(tagNameOfCompaignCost, "strong");

            //Compare sizes main price and compaign price
            Assert.IsTrue((hieghtCostOnMainPage < hieghtCompaignCostOnMainPage) && (widthCostOnMainPage < widthCompaignCostOnMainPage));

            //Go to compaign duck page
            driver.Navigate().GoToUrl("http://localhost/litecart/en/rubber-ducks-c-1/subcategory-c-2/yellow-duck-p-1");

            //Get parametrs for duck on product page
            string nameOnProductPage = driver.FindElement(By.CssSelector("#box-product .title")).Text;

            string costOnProductPage = driver.FindElement(By.CssSelector(".regular-price")).Text;
            int hieghtCostProductPage = driver.FindElement(By.CssSelector(".regular-price")).Size.Height;
            int widthCostOnProductPage = driver.FindElement(By.CssSelector(".regular-price")).Size.Width;
            string colorCostOnProductPage = driver.FindElement(By.CssSelector(".regular-price")).GetCssValue("color");
            tagNameOfCost = driver.FindElement(By.CssSelector(".regular-price")).TagName;

            string compaignCostOnProductPage = driver.FindElement(By.CssSelector(".campaign-price")).Text;
            int hieghtCompaignCostOnProductPage = driver.FindElement(By.CssSelector(".campaign-price")).Size.Height;
            int widthCompaignCostOnProductPage = driver.FindElement(By.CssSelector(".campaign-price")).Size.Width;
            string colorCompaignCostOnProductPage = driver.FindElement(By.CssSelector(".campaign-price")).GetCssValue("color");
            tagNameOfCompaignCost = driver.FindElement(By.CssSelector(".campaign-price")).TagName;

            //Check name
            Assert.AreEqual(nameOnMainPage, nameOnProductPage);

            //Check prices
            Assert.AreEqual(costOnMainPage, costOnProductPage);
            Assert.AreEqual(compaignCostOnMainPage, compaignCostOnProductPage);

            //Check color of main price and tag name on product page
            rgb = colorCostOnProductPage.Split(',');
            colorModel = rgb[0].Split('(')[0];

            Int32.TryParse(rgb[0].Split('(')[1], out r);
            Int32.TryParse(rgb[1], out g);
            Int32.TryParse(rgb[2], out b);

            //Choose check depending on color model because in Chrome: grei color is R=G=B and RGB != 0.
            // in Edge, Firefox: grei color is R=G and B = 0.

            switch (colorModel)
            {

                case "rgba":
                    {
                        Assert.IsTrue((r == g) && (g == b));
                        break;
                    }
                case "rgb":
                    {
                        Assert.IsTrue((r == g) && (b == 0));
                        break;
                    }
            }

            Assert.AreEqual(tagNameOfCost, "s");

            //Check color of compaign price and tag name on product page
            rgb = colorCompaignCostOnProductPage.Split(',');

            Int32.TryParse(rgb[0].Split('(')[1], out r);
            Int32.TryParse(rgb[1], out g);
            Int32.TryParse(rgb[2], out b);
            Assert.IsTrue((r != 0) && (g == 0) && (b == 0));
            Assert.AreEqual(tagNameOfCompaignCost, "strong");

            //Compare sizes main price and compaign price on product page
            Assert.IsTrue((hieghtCostOnMainPage < hieghtCompaignCostOnMainPage) && (widthCostOnMainPage < widthCompaignCostOnMainPage));

        }

        [SetUp]
        public void Start()
        {
            driver = new TWebDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);

            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
