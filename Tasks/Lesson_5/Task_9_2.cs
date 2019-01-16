using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace Lesson_5
{
    [TestFixture]
    class Task_9_2
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Test]
        public void TestTask9_2()
        {
            IList<IWebElement> allGeoZones;
            IList<IWebElement> allZonesRowsEditMode;
            List<string> allZones = new List<string>();
            List<string> allZonesTemplate = new List<string>();
            List<string> allCountriesUrls = new List<string>();

            HelperLogin.LogInAdmin(driver);
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");
            allGeoZones = driver.FindElements(By.CssSelector("[name=geo_zones_form] a[title]"));

            foreach (IWebElement zone in allGeoZones)
            {
                allCountriesUrls.Add(zone.GetAttribute("href"));
            }


            foreach (string url in allCountriesUrls)
            {
                driver.Navigate().GoToUrl(url);
                allZonesRowsEditMode = driver.FindElements(By.CssSelector("#table-zones td:nth-child(3) [selected]"));

                foreach(IWebElement row in allZonesRowsEditMode)
                {
                    allZones.Add(row.Text);
                }

                allZonesTemplate.AddRange(allZones);
                allZonesTemplate.Sort();

                Assert.IsTrue(allZones.SequenceEqual(allZonesTemplate));

                allZonesTemplate.Clear();
                allZones.Clear();
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
