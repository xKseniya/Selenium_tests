using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Selenium_Tests
{

    public class litecart_login_Edge
    {
        private IWebDriver driver;


        [SetUp]
        public void start()
        {
            driver = new EdgeDriver();

        }

        [Test]
        public void LitecartEdge()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            Thread.Sleep(400);

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
    public class litecart_login_FireFox
    {
        private IWebDriver driver;
   
        [SetUp]
        public void start()
        {
            driver = new FirefoxDriver();
         }

        [Test]
        public void LitecartFireFox()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            Thread.Sleep(400);

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }

}

