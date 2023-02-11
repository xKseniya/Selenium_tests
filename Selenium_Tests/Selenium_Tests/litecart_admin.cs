using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium_Tests
{
    internal class litecart_admin
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
        public void LitecartClickAll()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            Thread.Sleep(400);

            By Locator = By.Id("app-");
            By Docs = By.TagName("li");
            var elements = driver.FindElements(Locator);
            int a = elements.Count();

          
            for (int i = 0; i < a; i++)
            {
                elements[i].Click();
                elements = driver.FindElements(Locator);
                driver.FindElement(By.TagName("h1"));

                var DocsElement = elements[i].FindElements(Docs);
                for (int j = 0; j < DocsElement.Count(); j++)
                {
                    DocsElement[j].Click();
                    elements = driver.FindElements(Locator);
                    DocsElement = elements[i].FindElements(Docs);
                    Thread.Sleep(200);
                    driver.FindElement(By.TagName("h1"));
                }


                    Thread.Sleep(400);
            }
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
