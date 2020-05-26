using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace PageObjects
{
    class Login
    {

        private IWebDriver driver;
        public Login(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement homepage => driver.FindElement(By.CssSelector("h2"));   // страница "Login" или "Home Page"
        private IWebElement logout => driver.FindElement(By.XPath("//a[text()='Logout']"));

        public string PageAutorization()
        {
            return homepage.Text;
        }

        public void Logout()
        {
            logout.Click();            
        }
        public string CheckExitLogin() { return homepage.Text; }
    }
}

    
