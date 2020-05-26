using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PageObjects
{
    class NewProducts
    {
        private IWebDriver driver;
        public NewProducts(IWebDriver driver)
        {
            this.driver = driver;
        }


        private IWebElement linkAllProducts => driver.FindElement(By.XPath("//a[contains(text(), 'All Products')]")); // ссылка "All Products"
        private IWebElement buttonCreateNew => driver.FindElement(By.XPath("//a[contains(text(), 'Create new')]")); // Кнопка "создать новый продукт"
        private IWebElement sendKeyProductName => driver.FindElement(By.Id("ProductName")); // Поле ввода названия продукта
        private IWebElement selectCategoryId => driver.FindElement(By.Id("CategoryId"));  // Поле выбора категории продукта        
        private IWebElement selectSupplierId => driver.FindElement(By.Id("SupplierId")); // Поле выбора поставщика...
        private IWebElement sendKeyUnitPrice => driver.FindElement(By.Id("UnitPrice"));
        private IWebElement sendKeyQuantityPerUnit => driver.FindElement(By.Id("QuantityPerUnit"));
        private IWebElement sendKeyUnitsInStock => driver.FindElement(By.Id("UnitsInStock"));
        private IWebElement sendKeyUnitsOnOrder => driver.FindElement(By.Id("UnitsOnOrder"));
        private IWebElement sendKeyReorderLevel => driver.FindElement(By.Id("ReorderLevel"));
        private IWebElement checkboxDiscontinued => driver.FindElement(By.Id("Discontinued"));
        private IWebElement buttonSend => driver.FindElement(By.XPath("//input[@type=\"submit\"]"));
        private IWebElement productEditingPage => driver.FindElement(By.CssSelector("h2"));   // страница Product Editing
        




        // СОЗДАЕМ МЕТОДЫ
        public void CreateNewProductsName(string productDescription)  // Переходим по ссылкам All Products => Create New=> Создаем новое имя продукта
        {
            linkAllProducts.Click();
            buttonCreateNew.Click();
            sendKeyProductName.SendKeys(productDescription);
        }
        public string CheckProductEditingPage() { return productEditingPage.Text; }
        public void SelectNewCategoryId(string productDescription) { new SelectElement(selectCategoryId).SelectByText(productDescription); } // Выбираем категорию для нового продукта...
        public void SelectNewSupplierId(string productDescription) { new SelectElement(selectSupplierId).SelectByText(productDescription); }
        public void SendKeyNewUnitPrice(string productDescription) { sendKeyUnitPrice.SendKeys(productDescription); }
        public void SendKeyNewQuantityPerUnit(string productDescription) { sendKeyQuantityPerUnit.SendKeys(productDescription); }
        public void SendKeyNewUnitsInStock(string productDescription) { sendKeyUnitsInStock.SendKeys(productDescription); }
        public void SendKeyNewUnitsOnOrder(string productDescription) { sendKeyUnitsOnOrder.SendKeys(productDescription); }
        public string SearchLinkProductName(string ProductName)  
        {
            return driver.FindElement(By.XPath($"//a[text()=\"{ProductName}\"]")).Text;
        }


        public AllProductsPage SendKeyNewReorderLevel(string productDescription) // + отмечаем скидку,нажимаем "отправить" и переходим на страницу AllProducts
        {            
            new Actions(driver).Click(sendKeyReorderLevel).SendKeys(productDescription).Build().Perform();
                       
            new Actions(driver).Click(checkboxDiscontinued).Build().Perform();
          
            new Actions(driver).Click(buttonSend).SendKeys(Keys.Enter).Build().Perform();    

            return new AllProductsPage(driver);
        }

    }
}

