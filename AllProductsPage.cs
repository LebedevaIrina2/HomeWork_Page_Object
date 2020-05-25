using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PageObjects
{
    class AllProductsPage
    {
        private IWebDriver driver;
        public AllProductsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement linkAllProducts => driver.FindElement(By.XPath("//a[contains(text(), 'All Products')]")); // ссылка "All Products"
        private IWebElement searchProductName => driver.FindElement(By.XPath("//a[text()=\"King prawns\"]")); // Поле ввода названия продукта
        private IWebElement searchCategoryId => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[1])"));  // Поле выбора категории продукта        
        private IWebElement searchSupplierId => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[2])")); // Поле выбора поставщика...
        private IWebElement searchQuantityPerUnit => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[3])"));
        private IWebElement searchUnitPrice => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[4])"));
        private IWebElement searchUnitsInStock => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[5])"));
        private IWebElement searchUnitsOnOrder => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[6])"));
        private IWebElement searchReorderLevel => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[7])"));
        private IWebElement searchDiscontinued => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[8])"));
        private IWebElement productEditingNamePage => driver.FindElement(By.CssSelector("h2"));   // Заголовок страницы "All Products"

        public string SelectProductName()   //Проверяем строку с названием продукта
        {
            new Actions(driver).Click(linkAllProducts).SendKeys(Keys.Enter).Build().Perform();

            return searchProductName.Text; 
        }
        public string CheckNameAllProductsPage() { return productEditingNamePage.Text; } //Проверяем, что мы на нужной страницу "All Products"
        public string CheckCategoryID() { return searchCategoryId.Text; }  //Проверяем строку с названием категории продуктов...
        public string CheckSupplierID() { return searchSupplierId.Text; }
        public string CheckQuantityPerUnit() { return searchQuantityPerUnit.Text; }
        public string CheckUnitPrice() { return searchUnitPrice.Text; }         
        public string CheckUnitsInStock() { return searchUnitsInStock.Text; }
        public string CheckUnitsOnOrder() { return searchUnitsOnOrder.Text; }
        public string CheckReorderLevel() { return searchReorderLevel.Text; }
        public string CheckDiscontinuedl() { return searchDiscontinued.Text; }
                        
    }
}
