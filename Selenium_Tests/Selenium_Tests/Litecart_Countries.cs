using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics.Metrics;

namespace Selenium_Tests
{
    internal class Litecart_Countries
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]

        public void LitecartCountries()
        {
            // Заходим в админку
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            Thread.Sleep(400);

            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";


            var Rows = driver.FindElements(By.XPath("//table[@class='dataTable']/tbody/tr[@class='row']"));
            List<string> Countries = new List<string>();
            List<string> Links = new List<string>();

            for (int k = 0; k < Rows.Count(); k++)
            {
                var row = Rows[k];
                var country = row.FindElement(By.XPath("./td[5]/a")).GetAttribute("textContent");
                Countries.Add(country);

                var zone = row.FindElement(By.XPath("./td[6]")).GetAttribute("textContent");
                if (zone != "0") Links.Add(row.FindElement(By.XPath("./td[5]/a")).GetAttribute("href"));

            }

            
            // Проверяем сортировку стран
            List<string> CountriesSorted = new List<string>(Countries);
            CountriesSorted.Sort();

            if (Countries.SequenceEqual(CountriesSorted)) Console.WriteLine("Countries are sorted correctly");
            else Console.WriteLine("Countries are not sorted correctly");


            // Проверяем сортировку в зонах
            foreach (string link in Links)
            {
                driver.Url = link;

                var Names = driver.FindElements(By.XPath("//table[@id='table-zones']/tbody/tr/td[3]"));
                List<string> ZoneNames = new List<string>();

                foreach (IWebElement name in Names)
                {
                    if (name.GetAttribute("textContent") != "")
                    {
                        ZoneNames.Add(name.GetAttribute("textContent"));
                    }
                }

                List<string> ZoneNamesSorted = new List<string> (ZoneNames);
                ZoneNamesSorted.Sort();
                if (ZoneNames.SequenceEqual(ZoneNamesSorted)) Console.WriteLine("Zones are sorted correctly");
                else Console.WriteLine("Zones are not sorted correctly");

               
            }


        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
