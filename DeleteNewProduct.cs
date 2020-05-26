using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PageObjects
{
    class DeleteNewProduct
    {
        private IWebDriver driver;
        public DeleteNewProduct(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement AllProductsPage => driver.FindElement(By.XPath("//a[contains(text(), 'All Products')]")); // ссылка "All Products"
                
                
        public void RemoveProducts(string ProductName)
        {

            AllProductsPage.Click();
            new Actions(driver).Click(driver.FindElement(By.XPath($"//a[text()=\"{ProductName}\"]"))).SendKeys(Keys.Tab + Keys.Tab + Keys.Enter).Build().Perform(); // Кликаем по кнопке "remove"  // Переход по ссылке "All Products"         
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.SwitchTo().Alert().Accept(); //Подтверждаем удаление в всплывающем окне предупреждения       

        }

    }
}
