﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace PageObjects
{
       public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private MainPage mainPage;
        private Login login;
        private AllProductsPage allProducts;          
        private NewProducts newProducts;
        private DeleteNewProduct deleteNewProduct;
           
        // Для проверки загрузки нужной страницы
        private const string CheckNameHomePage="Home page"; 
        private const string CheckNameLoginPage = "Login";
        private const string CheckNameProductEditingPage = "Product editing";

        //  Для авторизации
        private const string SelectLogin = "user";  

        // Данные для карточки с новым продуктом
        private const string SendKeysProductName = "King prawns";  
        private const string SelectCategoryId = "Seafood";
        private const string SelectSupplierId = "Pavlova, Ltd.";
        private const string SendKeysUnitPrice = "500";
        private const string SendKeysUnitPriceCheck = "500,0000";
        private const string SendKeysQuantityPerUnit = "24 pieces";
        private const string SendKeysUnitsInStock = "20";
        private const string SendKeysUnitsOnOrder = "3";
        private const string SendKeysReorderLevel = "2";

      

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:5000");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
           
            mainPage = new MainPage(driver);    //инициализируем страницу MainPage
            login = new Login(driver);
        }


        //// Autorization
        [Test]
        public void Test1_Autorization()   
        {
            Assert.AreEqual(CheckNameLoginPage, login.PageAutorization()); // Проверка загрузки нужной страницы авторизации с текстом "Login" 
                        
            mainPage.LoginEnter(SelectLogin); // вызываем метод ввода логина
            login=mainPage.PasswordAndAutorization(SelectLogin); // вызываем метод ввода пароля и клика по кнопке "Отправить". Переход на стр.Login

            Assert.AreEqual(CheckNameHomePage, login.PageAutorization());// Проверка успешной авторизации - должна быть загружена страница "Home page"
        }


        // Adding a New Product
        [Test]
        public void Test2_Add_Product()
        {
            mainPage.LoginEnter(SelectLogin); // вызываем метод ввода логина
            login = mainPage.PasswordAndAutorization(SelectLogin); // вызываем метод ввода пароля и клика по кнопке "Отправить" и переходим на страницу Product editing
           
            allProducts = new AllProductsPage(driver);
            newProducts = new NewProducts(driver);

            // ЗАПОЛНЯЕМ КАРТОЧКУ ПРОДУКТА
            newProducts.CreateNewProductsName(SendKeysProductName);
            Assert.AreEqual(CheckNameProductEditingPage, newProducts.CheckProductEditingPage()); // Проверка загрузки нужной страницы "Product editing"
            newProducts.SelectNewCategoryId(SelectCategoryId);
            newProducts.SelectNewSupplierId(SelectSupplierId);
            newProducts.SendKeyNewUnitPrice(SendKeysUnitPrice);
            newProducts.SendKeyNewQuantityPerUnit(SendKeysQuantityPerUnit);
            newProducts.SendKeyNewUnitsInStock(SendKeysUnitsInStock);
            newProducts.SendKeyNewUnitsOnOrder(SendKeysUnitsOnOrder); // +отмечаем скидку и нажимаем кнопку "Отправить"
            allProducts= newProducts.SendKeyNewReorderLevel(SendKeysReorderLevel); // + отмечаем скидку,нажимаем "отправить" и переходим на страницу AllProducts
            Assert.AreEqual(SendKeysProductName, newProducts.SearchLinkProductName(SendKeysProductName)); // Проверка успешного создания продукта.
        }


        //Test 3. New Product. Check Values
        [Test]
        public void Test3_Check_Value()
        {
            mainPage.LoginEnter(SelectLogin); // вызываем метод ввода логина
            login = mainPage.PasswordAndAutorization(SelectLogin); // вызываем метод ввода пароля и клика по кнопке "Отправить" и переходим на страницу HomePage

            allProducts = new AllProductsPage(driver);

            allProducts.SelectAllProducts(); // заходим на стр. "All Products"   
            allProducts.SelectLinkProductName(SendKeysProductName); //Заходим в карточку продукта
           
            Assert.AreEqual(SendKeysProductName, allProducts.CheckProductName()); // Проверка названия продукта
            Assert.AreEqual(SelectCategoryId, allProducts.CheckCategoryId());
            Assert.AreEqual(SelectSupplierId, allProducts.CheckSupplierID());
            Assert.AreEqual(SendKeysQuantityPerUnit, allProducts.CheckQuantityPerUnit());
            Assert.AreEqual(SendKeysUnitPriceCheck, allProducts.CheckUnitPrice());
            Assert.AreEqual(SendKeysUnitsInStock, allProducts.CheckUnitsInStock());
            Assert.AreEqual(SendKeysUnitsOnOrder, allProducts.CheckUnitsOnOrder());
            Assert.AreEqual(SendKeysReorderLevel, allProducts.CheckReorderLevel());
            Assert.AreEqual("true", allProducts.CheckDiscontinuedl()); 
        }



        //Test 4. Delete Created Products
        [Test]
        public void Test4_Delete()
        {
            
            mainPage.LoginEnter(SelectLogin); // вызываем метод ввода логина
            login = mainPage.PasswordAndAutorization(SelectLogin); // вызываем метод ввода пароля и клика по кнопке "Отправить" и переходим на страницу HomePage

            deleteNewProduct = new DeleteNewProduct(driver);

            deleteNewProduct.RemoveProducts (SendKeysProductName);
            Assert.AreEqual(false ,  deleteNewProduct.isElementPresent(SendKeysProductName)); // Проверка успешного удаления продукта

        }


        //Test 5. Logout
        [Test]
        public void Test5_Logout()
        {
            mainPage.LoginEnter(SelectLogin); // вызываем метод ввода логина
            login = mainPage.PasswordAndAutorization(SelectLogin); // вызываем метод ввода парол, кликаем по кнопке "Отправить" и переходим на страницу HomePage


            login.Logout(); //LOGOUT
            login.CheckExitLogin(); // Проверка выхода из аккаунта. Подтверждение загрузки страницы  "Login"
                        
            mainPage.LoginEnter(SelectLogin); // заново вводим логин
            login = mainPage.PasswordAndAutorization(SelectLogin); // заново вводим пароль,  кликаем по кнопке "Отправить" и переходим на страницу HomePage
        }


        // Закрыть окно браузера после выполнения теста
        [TearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}

    


