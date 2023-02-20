using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using static System.Net.Mime.MediaTypeNames;


namespace Selenium_Tests
{
    internal class Litecart_check_page
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            //тут можно переключить драйверы

            driver = new ChromeDriver();
            //driver = new EdgeDriver();
            //driver = new FirefoxDriver();
        
        //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

        // функция для разбивки строки RGBa
        public static List<int> SplitRGB(string s)
        {
            char[] separators = new char[] { ' ', ',', ')', '(' };
            string[] subs = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            List<int> result = new List<int>();

            for (int j = 1; j <= 4; j++)
            {
                result.Add(Int32.Parse(subs[j]));
            }

            return result;
        }

        [Test]

        public void Litecart_page_check()
        {
            Dictionary<string, string> mainPage = new Dictionary<string, string>();
            Dictionary<string, string> productPage = new Dictionary<string, string>();


            // Сбор данных для главной страницы
            driver.Url = "http://localhost/litecart/";
           
            var element = driver.FindElement(By.XPath("//div[@id='box-campaigns']/div/ul/li"));
            var full_price = element.FindElement(By.XPath("./a[1]/div[@class='price-wrapper']/s[@class='regular-price']"));
            var sale_price = element.FindElement(By.XPath("./a[1]/div[@class='price-wrapper']/strong[@class='campaign-price']"));
                        
            mainPage.Add("name", element.FindElement(By.XPath("./a[1]/div[@class='name']")).GetAttribute("textContent"));
            mainPage.Add("full_price", full_price.GetAttribute("textContent"));
            mainPage.Add("sale_price", sale_price.GetAttribute("textContent"));

            var full_price_color = SplitRGB(full_price.GetCssValue("color"));
            var sale_price_color = SplitRGB(sale_price.GetCssValue("color"));
            var full_price_dec = full_price.GetCssValue("text-decoration");
            var sale_price_weight = Int32.Parse(sale_price.GetCssValue("font-weight"));
            var full_price_weight = Int32.Parse(full_price.GetCssValue("font-weight"));

            var sale_price_size = Convert.ToDouble(sale_price.GetCssValue("font-size").Trim(new char[] { 'p', 'x' }).Replace(".", ","));
            var full_price_size = Convert.ToDouble(full_price.GetCssValue("font-size").Trim(new char[] { 'p', 'x' }).Replace(".", ","));
          
            // Главная страница - акционная цена должна быть крупнее
            NUnit.Framework.Assert.That(sale_price_size, Is.GreaterThan(full_price_size));

            //Сбор данных для страницы продукта
            driver.Url = "http://localhost/litecart/en/rubber-ducks-c-1/subcategory-c-2/yellow-duck-p-1";

            var full_price_p = driver.FindElement(By.XPath("//div[@class='information']/div/s"));
            var sale_price_p = driver.FindElement(By.XPath("//div[@class='information']/div/strong"));

            productPage.Add("name", driver.FindElement(By.XPath("//h1")).GetAttribute("textContent"));
            productPage.Add("full_price", full_price_p.GetAttribute("textContent"));
            productPage.Add("sale_price", sale_price_p.GetAttribute("textContent"));

            var full_price_color_p = SplitRGB(full_price_p.GetCssValue("color"));
            var sale_price_color_p = SplitRGB(sale_price_p.GetCssValue("color"));
            var full_price_dec_p = full_price_p.GetCssValue("text-decoration");
            var sale_price_weight_p = Int32.Parse(sale_price_p.GetCssValue("font-weight"));
            var full_price_weight_p = Int32.Parse(full_price_p.GetCssValue("font-weight"));

            var sale_price_size_p = Convert.ToDouble(sale_price_p.GetCssValue("font-size").Trim(new char[] { 'p', 'x' }).Replace(".", ","));
            var full_price_size_p = Convert.ToDouble(full_price_p.GetCssValue("font-size").Trim(new char[] { 'p', 'x' }).Replace(".", ","));


            // Страница продукта - акционная цена должна быть крупнее
            NUnit.Framework.Assert.That(sale_price_size_p, Is.GreaterThan(full_price_size_p));

            // Проверяем, что название товара и цены совпадают на главной странице и странице товара
            NUnit.Framework.Assert.That(mainPage["name"] ==  productPage["name"]);
            NUnit.Framework.Assert.That(mainPage["full_price"] == productPage["full_price"]);
            NUnit.Framework.Assert.That(mainPage["sale_price"] == productPage["sale_price"]);

            // Проверяем, что обычная цена серая и зачеркнутая
            NUnit.Framework.Assert.That((full_price_color[0] == full_price_color[1]) && (full_price_color[1] == full_price_color[2]));
            NUnit.Framework.Assert.That(full_price_dec.Contains("line-through"));
            NUnit.Framework.Assert.That((full_price_color_p[0] == full_price_color_p[1]) && (full_price_color_p[1] == full_price_color_p[2]));
            NUnit.Framework.Assert.That(full_price_dec_p.Contains("line-through"));

            // Проверяем, что акционная цена красная и жирная
            NUnit.Framework.Assert.That((sale_price_color[0] != 0) && (sale_price_color[1] == 0) && (sale_price_color[2] == 0));
            NUnit.Framework.Assert.That(sale_price_weight > full_price_weight);
            NUnit.Framework.Assert.That((sale_price_color_p[0] != 0) && (sale_price_color_p[1] == 0) && (sale_price_color_p[2] == 0));
            NUnit.Framework.Assert.That(sale_price_weight_p > full_price_weight_p);


        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
