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
    internal class Litecart_add_new
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();

        }
        [Test]

        public void LitecartAddProduct()
        {
        
            // Входим в админку
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            // Посчитаем, сколько товаров с нашим именем уже есть в каталоге
            driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            var elements = driver.FindElements(By.XPath("//table[@class='dataTable']//tr//td//a"));
            int elemCount = 0;
            foreach (IWebElement element in elements)
            {
                if (element.GetAttribute("textContent") == "Queen Duck") elemCount++;
            }
            
            // Заходим в "add new product"
            driver.FindElement(By.XPath("//ul[@id='box-apps-menu']/li[2]/a")).Click();
            driver.FindElement(By.XPath("//td[@id='content']/div[1]/a[2]")).Click();


            // Заполняем General
            string imgPath = Path.GetFullPath("1-queen-duck.png");

            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[1]/td/label[1]/input")).Click();
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[2]/td/span/input")).SendKeys("Queen Duck");
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[3]/td/input")).SendKeys("qd001");
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[4]/td/div/table/tbody/tr[2]/td[1]/input")).Click();
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[4]/td/div/table/tbody/tr[1]/td[1]/input")).Click();
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[8]//input")).SendKeys("100");

            SelectElement statusSelect = new SelectElement(driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[8]//select[@name='sold_out_status_id']")));
            statusSelect.SelectByValue("2");

            
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[10]//input")).SendKeys("17022023");
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[11]//input")).SendKeys("17122023");
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[9]//input")).SendKeys(imgPath);
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[9]//a")).Click();

            // переходим в Information
            driver.FindElement(By.XPath("//ul[@class='index']/li[2]/a")).Click();
            Thread.Sleep(500);

            SelectElement manufacturer = new SelectElement(driver.FindElement(By.XPath("//select[@name='manufacturer_id']")));
            manufacturer.SelectByValue("1");
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("queen");
            driver.FindElement(By.XPath("//input[@name='short_description[en]']")).SendKeys("test");
            driver.FindElement(By.XPath("//div[@class='trumbowyg-editor']")).SendKeys("test");
            driver.FindElement(By.XPath("//input[@name='head_title[en]']")).SendKeys("test");
            driver.FindElement(By.XPath("//input[@name='meta_description[en]']")).SendKeys("test");

            // Переходим в Prices
            driver.FindElement(By.XPath("//ul[@class='index']/li[4]/a")).Click();
            Thread.Sleep(500);

            driver.FindElement(By.XPath("//input[@name='purchase_price']")).SendKeys("40");
            SelectElement price = new SelectElement(driver.FindElement(By.XPath("//select[@name='purchase_price_currency_code']")));
            price.SelectByValue("USD");
    

            driver.FindElement(By.XPath("//input[@name='prices[USD]']")).SendKeys("40");
            driver.FindElement(By.XPath("//input[@name='prices[EUR]']")).SendKeys("40");
            
            
            driver.FindElement(By.XPath("//button[@name='save']")).Click();
            Thread.Sleep(500);

            // Проверим, что новый товар с нашим именем появился в админке
            driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            elements = driver.FindElements(By.XPath("//table[@class='dataTable']//tr//td//a"));
            int newElemCount = 0;
            foreach (IWebElement element in elements)
            {
                if (element.GetAttribute("textContent") == "Queen Duck") newElemCount++;
            }
            Assert.AreEqual(newElemCount, elemCount +1);
            


        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

    }
}
