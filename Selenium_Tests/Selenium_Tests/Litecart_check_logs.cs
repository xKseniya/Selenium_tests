using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Assert = NUnit.Framework.Assert;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Selenium_Tests
{
    internal class Litecart_check_logs
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }
        [Test]

        public void LitecartCheckLogs()
        {

            // Входим в админку
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            // Пройдем по всем товарам в каталоге
            driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            var elements = driver.FindElements(By.XPath("//table[@class='dataTable']//tr/td[3]/a"));
            
            for (int i = 0; i < elements.Count; i++)
            {
                var element = elements[i];
                
                // Если зашли в подкатегорию
                if (element.GetAttribute("textContent") == "Subcategory")
                {
                    var URL = element.GetAttribute("href");
                    var elemCount = elements.Count;

                    element.Click();

                    elements = driver.FindElements(By.XPath("//table[@class='dataTable']//tr/td[3]/a"));
                    var newElemCount = elements.Count;
                    var num = i;

                    for(int j = 0; j <= (newElemCount - elemCount); j++) 
                    {
                        elements[num].Click();
                        /*
                        foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
                        {
                            Console.WriteLine(l);
                        }
                        */
                        Assert.AreEqual(0, driver.Manage().Logs.GetLog("browser").Count);
                        
                        driver.Url = URL;
                        elements = driver.FindElements(By.XPath("//table[@class='dataTable']//tr/td[3]/a"));
                        num++;

                    }

                    driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";
                    elements = driver.FindElements(By.XPath("//table[@class='dataTable']//tr/td[3]/a"));
                }
                else
                {
                    element.Click();
                    Assert.AreEqual(0, driver.Manage().Logs.GetLog("browser").Count);
                    /*
                    foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
                    {
                        Console.WriteLine(l);
                    }
                    */
                    driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";
                    elements = driver.FindElements(By.XPath("//table[@class='dataTable']//tr/td[3]/a"));
                    
                }
                                                
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
