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
    public class Task_9_1
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [Test]
        public void TestTask9_1()
        {
            IList<IWebElement> allCountriesRows;
            List<string> allCountriesNames = new List<string>();
            List<string> allCountriesTemplate = new List<string>();
            IList<string> linksForCountryWithTimeZones = new List<string>();
            IList<IWebElement> allZoneRows;
            List<string> allZonesForCurrentCountry = new List<string>();
            List<string> allZonesForCurrentCountryTemplate = new List<string>(); ;
            IWebElement element;

            HelperLogin.LogInAdmin(driver);
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");
          
            //Get all rows with country names
            allCountriesRows = driver.FindElements(By.CssSelector("[name=countries_form] tr.row"));
            
            foreach(IWebElement row in allCountriesRows)
            {
                //Get name of each country and link to edit page if country has zones > 0.
                element = row.FindElement(By.CssSelector("a"));
                allCountriesNames.Add(element.Text);

                if (!row.FindElement(By.CssSelector("td:nth-of-type(6)")).Text.Equals("0"))
                {
                    linksForCountryWithTimeZones.Add(element.GetAttribute("href"));
                }              
            }

            //Check sort for Countries
            allCountriesTemplate.AddRange(allCountriesNames);
            allCountriesTemplate.Sort();

            Assert.IsTrue(allCountriesNames.SequenceEqual(allCountriesTemplate));

            //Check sort of Zones
            foreach(string url in linksForCountryWithTimeZones)
            {
                driver.Navigate().GoToUrl(url);
                allZoneRows = driver.FindElements(By.XPath("//table[@id = 'table-zones'] // input[contains(@name, '[name]') and @type='hidden']"));
                
                //Get zone names and check sort
                foreach(IWebElement zone in allZoneRows)
                {
                    allZonesForCurrentCountry.Add(zone.GetAttribute("value"));
                }

                allZonesForCurrentCountryTemplate.AddRange(allZonesForCurrentCountry);
                allZonesForCurrentCountryTemplate.Sort();

                Assert.IsTrue(allZonesForCurrentCountry.SequenceEqual(allZonesForCurrentCountryTemplate));

                allZonesForCurrentCountryTemplate.Clear();
                allZonesForCurrentCountry.Clear();
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
