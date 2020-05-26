using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PageObjects
{
    class MainPage
    {
        private IWebDriver driver;
        public MainPage(IWebDriver driver)
        { 
        this.driver = driver;
        }


        private IWebElement sendKeyLogin => driver.FindElement(By.XPath("//input[@id=\"Name\"]"));  // поле ввода логина

        private IWebElement sendKeyPassword => driver.FindElement(By.XPath("//input[@id=\"Password\"]")); // поле ввода пароля

        private IWebElement LoginButton => driver.FindElement(By.XPath("//input[@type='submit']")); // Кнопка "Отправить" Login

        public void LoginEnter(string login)    //метод для ввода логина  
        {           
            new Actions(driver).Click(sendKeyLogin).SendKeys(login).Build().Perform();         
        }

        public  Login PasswordAndAutorization(string password) //создаем метод для ввода пароля, клика по кнопке "Отправить" и возвр-м страницу "Login"
        {
            new Actions(driver).Click(sendKeyPassword).SendKeys(password).Build().Perform(); // Вводим пароль

            new Actions(driver).Click(LoginButton).SendKeys(Keys.Enter).Build().Perform(); //Нажимаем кнопку "Отправить"


            return new Login(driver);

        }
    }
}
