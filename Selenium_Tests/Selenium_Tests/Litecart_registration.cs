using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace Selenium_Tests
{
    internal class Litecart_registration
    {
        internal class Litecart_check_page
        {
            private IWebDriver driver;
            private WebDriverWait wait;

            [SetUp]
            public void start()
            {
                driver = new ChromeDriver();

            }

          

            [Test]

            public void LitecartRegistration()
            {
                /*
                 1) регистрация новой учётной записи с достаточно уникальным адресом электронной почты (чтобы не конфликтовало с ранее созданными пользователями, в том числе при предыдущих запусках того же самого сценария),
2) выход (logout), потому что после успешной регистрации автоматически происходит вход,
3) повторный вход в только что созданную учётную запись,
4) и ещё раз выход.

В качестве страны выбирайте United States, штат произвольный. При этом формат индекса -- пять цифр.
                 */
                Random rnd = new Random();
                int value = rnd.Next(1000, 9999);
                driver.Url = "http://localhost/litecart/en/create_account";
                Thread.Sleep(1000);

                var inputName = driver.FindElement(By.XPath("//input[@name='firstname']"));
                var inputLastName = driver.FindElement(By.XPath("//input[@name='lastname']"));
                var inputAddress1 = driver.FindElement(By.XPath("//input[@name='address1']"));
                var inputPostcode = driver.FindElement(By.XPath("//input[@name='postcode']"));
                var inputEmail = driver.FindElement(By.XPath("//input[@name='email']"));
                var inputPhone = driver.FindElement(By.XPath("//input[@name='phone']"));
                var inputPassword = driver.FindElement(By.XPath("//input[@name='password']"));
                var inputCity = driver.FindElement(By.XPath("//input[@name='city']"));
                var confirmPassword = driver.FindElement(By.XPath("//input[@name='confirmed_password']"));
                var country = driver.FindElement(By.XPath("//select[@name='country_code']"));
                var state = driver.FindElement(By.XPath("//select[@name='zone_code']"));
                var submit = driver.FindElement(By.XPath("//button[@type='submit']"));

                string name = "Firstname";
                string lastName = "Lastname";
                string address = "Home address 5";
                string postcode = "08724";
                string city = "Test";
                string email = "user" + value + "@mail.me";
                string phone = "+19877" + value;
                string password = "test";

                // Заполняем поля
                inputName.SendKeys(name);
                inputLastName.SendKeys(lastName);
                inputAddress1.SendKeys(address);
                inputPostcode.SendKeys(postcode);
                inputCity.SendKeys(city);
                inputEmail.SendKeys(email);
                inputPhone.SendKeys(phone);

                SelectElement countrySelect = new SelectElement(country);
                countrySelect.SelectByText("United States");
                Thread.Sleep(3000);
                SelectElement stateSelect = new SelectElement(state);
                stateSelect.SelectByText("South Dakota");
                inputPassword.SendKeys(password);
                confirmPassword.SendKeys(password);

                submit.Click();
                Thread.Sleep(3000);

                Assert.AreNotEqual(driver.Url, "http://localhost/litecart/en/create_account");

                // выходим из учетки
                var logout = driver.FindElement(By.XPath("//div[@id='box-account']/div/ul/li[4]/a"));
                logout.Click();
                Thread.Sleep(3000);

                // заходим в учетку
                driver.FindElement(By.XPath("//input[@name='email']")).SendKeys(email);
                driver.FindElement(By.XPath("//input[@name='password']")).SendKeys(password);
                driver.FindElement(By.XPath("//button[@name='login']")).Click();
                Thread.Sleep(3000);

                // выходим из учетки
                logout = driver.FindElement(By.XPath("//div[@id='box-account']/div/ul/li[4]/a"));
                logout.Click();
                Thread.Sleep(3000);


            }

            [TearDown]
            public void stop()
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}
