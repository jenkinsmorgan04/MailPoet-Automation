using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using MailPoetAutomation.PageObjects;

namespace MailPoetAutomation
{
    [TestClass]
    public class TestCases
    {
        IWebDriver driver;
        string url = "http://automationpractice.com/index.php";
        [TestInitialize]
        public void TestSetup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            Assert.AreEqual("My Store", driver.Title);
        }
        //Cleanup used to run after each test and close browser
        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }
        [TestMethod]
        //The purpose of this Test case is to add a single item to the cart confirm the correct item was added and prices match, increase the quanity by one and confirm the prices again before clicking proceed to checkout
        public void AddItemToCart()
        {
            try
            {
                IWebElement selectedItem = driver.FindElement(By.XPath("//*[@id='homefeatured']/li[1]/div"));
                IWebElement addToCartButton = driver.FindElement(By.XPath("//*[@id='homefeatured']/li[1]/div/div[2]/div[2]/a[1]/span"));
                string selectedItemName = HomePage.RetreiveItemName(driver);
                string selectedItemPrice = HomePage.RetrieveItemPrice(driver);

                HomePage.AddItemToCart(driver, selectedItem, addToCartButton);
                HomePage.ConfirmItemDetailsOnOverlay(driver, selectedItemName);
                HomePage.ClickProceedToCheckoutOnOverlay(driver);
                CheckOutPage.ConfirmItemDetails(driver, selectedItemName, selectedItemPrice);
                CheckOutPage.ConfirmCartPrices(driver, selectedItemPrice);
                CheckOutPage.IncreaseQuantity(driver);
                CheckOutPage.ConfirmCartPrices(driver, selectedItemPrice);
                CheckOutPage.ClickProceedToCheckOut(driver);


            }
            catch (NoSuchElementException)
            {
                //If element doesnt exist take and savescreenshot.
                SaveScreenshot();
            }
        }
        [TestMethod]
        //The purpose of this test case is to search for a specific item, confirm that the item is present, select that item, confirm item details then add item to cart and confirm item details on checkout.
        public void SearchForItemConfirmThenAddToCart()
        {
            try
            {
                string searchItemName = "Printed Chiffon Dress";

                HomePage.SearchForItem(driver, searchItemName);
                SearchResultsPage.ConfirmSearchedItem(driver, searchItemName);
                string searchItemPrice = SearchResultsPage.RetrieveSearchItemPrice(driver);
                SearchResultsPage.SelectSearchedItem(driver, searchItemName);
                ProductPage.ConfirmSelectedItemDetails(driver, searchItemName, searchItemPrice);
                ProductPage.IncreaseQuantity(driver);
                ProductPage.AddToCart(driver);
                HomePage.ConfirmItemDetailsOnOverlay(driver, searchItemName);
                HomePage.ClickProceedToCheckoutOnOverlay(driver);
                CheckOutPage.ConfirmItemDetails(driver, searchItemName, searchItemPrice);
                CheckOutPage.ConfirmCartPrices(driver, searchItemPrice);
                CheckOutPage.ClickProceedToCheckOut(driver);
            }
            catch (NoSuchElementException)
            {
                //If element doesnt exist take and savescreenshot.
                SaveScreenshot();
            }
        }
        private void SaveScreenshot()
        {
            string startupPath = GetScreenShotPath();
            string screenShotPath = startupPath + "\\" + DateTime.Now.ToString("dd_MMMM_hh_mm_ss_tt") + ".png";
            string websitePath = "/ScreenShots/" + DateTime.Now.ToString("dd_MMMM_hh_mm_ss_tt") + ".png";
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(screenShotPath, ScreenshotImageFormat.Png);

        }
        private string GetScreenShotPath()
        {
            string virtualPath = "ScreenShots";
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            if (string.IsNullOrWhiteSpace(basePath)) basePath = AppDomain.CurrentDomain.BaseDirectory;

            return Path.Combine(basePath, virtualPath);
        }
        private bool IsElementPresent(By element)
        {
            try
            {
                driver.FindElement(element);
                return true;
            }
            catch (NoSuchElementException)
            {
                //If element doesnt exist take and savescreenshot.
                SaveScreenshot();
                return false;
            }
        }
    }
}
