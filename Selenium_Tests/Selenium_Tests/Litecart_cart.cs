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
    internal class Litecart_cart
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

        public void LitecartCart()
        {

            /*
                       
            1) открыть главную страницу
            2) открыть первый товар из списка
            2) добавить его в корзину (при этом может случайно добавиться товар, который там уже есть, ничего страшного)
            3) подождать, пока счётчик товаров в корзине обновится
            4) вернуться на главную страницу, повторить предыдущие шаги ещё два раза, чтобы в общей сложности в корзине было 3 единицы товара
            5) открыть корзину (в правом верхнем углу кликнуть по ссылке Checkout)
            6) удалить все товары из корзины один за другим, после каждого удаления подождать, пока внизу обновится таблица
            
             */
            driver.Url = "http://localhost/litecart/";
            
            // Добавляем товары
            for (int i = 0; i<3; i++)
            {
                driver.FindElement(By.XPath($"//ul[@class='listing-wrapper products']/li[{i+1}]/a[1]")).Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains("duck"));

                var quanity = (Int32.Parse(driver.FindElement(By.XPath("//span[@class='quantity']")).GetAttribute("textContent")) + 1).ToString();
                try 
                {
                    SelectElement statusSelect = new SelectElement(driver.FindElement(By.XPath("//select[@name='options[Size]']")));
                    statusSelect.SelectByValue("Small");
                }
                catch (NoSuchElementException)
                { }
    

                driver.FindElement(By.XPath("//button[@name='add_cart_product']")).Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.XPath("//span[@class='quantity']")), quanity));

                driver.Url = "http://localhost/litecart/";
            }

            // Удаляем товары

            driver.FindElement(By.XPath("//div[@id='cart']/a[@class='link']")).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains("checkout"));
            IWebElement? element = null;
            var cnt = driver.FindElements(By.XPath("//table[@class='dataTable rounded-corners']//td[@class='item']")).Count;

            for (int j = 0; j < cnt; j++)
            {
                var ourItem = driver.FindElement(By.XPath("//div[@style='display: inline-block;']//a")).GetAttribute("textContent");
                var items = driver.FindElements(By.XPath("//table[@class='dataTable rounded-corners']//td[@class='item']"));
                foreach (var item in items)
                {
                    if (item.GetAttribute("textContent") == ourItem) element = item;
                }
                driver.FindElement(By.XPath("//button[@value='Remove']")).Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
            }

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//em")));
            
        }
        

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
