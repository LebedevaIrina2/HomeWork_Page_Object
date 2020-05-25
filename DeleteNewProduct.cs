using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

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

        //private IWebElement removeNewProduct => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[10])"));
        private IWebElement NewProductName => driver.FindElement(By.XPath("//td/a[text()=\"King prawns\"]"));

        public void RemoveProducts()
        {
            new Actions(driver).Click(AllProductsPage).SendKeys(Keys.Enter).Build().Perform();           
            new Actions(driver).Click(NewProductName).SendKeys(Keys.Tab + Keys.Tab + Keys.Enter).Build().Perform(); // Кликаем по кнопке "remove"
            Thread.Sleep(500);
            driver.SwitchTo().Alert().Accept(); //Подтверждаем удаление в всплывающем окне предупреждения       

        }



    }
}
