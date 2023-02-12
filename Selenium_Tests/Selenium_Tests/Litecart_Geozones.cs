using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_Tests
{
    internal class Litecart_Geozones
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

        public void LitecartGeozones_check()
        {
            // Заходим в админку
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            Thread.Sleep(400);

            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

            var Rows = driver.FindElements(By.XPath("//tr[@class='row']/td[3]/a"));
            List<string> Links = new List<string>();

            for (int i = 0; i < Rows.Count(); i++)
            {
                var row = Rows[i];
                Links.Add(row.GetAttribute("href"));
            }


            // Проверяем сортировку в зонах
            foreach (string link in Links)
            {
                driver.Url = link;

                var Names = driver.FindElements(By.XPath("//table[@class='dataTable']/tbody/tr/td[3]/select/option[@selected='selected']"));
                List<string> ZoneNames = new List<string>();

                foreach (IWebElement name in Names)
                {
                    if (name.GetAttribute("textContent") != "")
                    {
                        ZoneNames.Add(name.GetAttribute("textContent"));
                    }
                }

                List<string> ZoneNamesSorted = new List<string>(ZoneNames);
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
