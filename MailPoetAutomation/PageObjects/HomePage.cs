using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPoetAutomation.PageObjects
{
    static class HomePage
    {
        public static void AddItemToCart(IWebDriver driver, IWebElement elementToHover, IWebElement elementToClick)
        {
            //Perform mouse hover and click to add item to cart.
            Actions action = new Actions(driver);
            action.MoveToElement(elementToHover).Click(elementToClick).Build().Perform();
        }
        public static void ConfirmItemDetailsOnOverlay(IWebDriver driver, string selectedItemName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.XPath("//*[@id='layer_cart']/div[1]/div[1]/h2")), "Product successfully added to your shopping cart"));
            //Confirm the correct item was added to cart.
            Assert.AreEqual(selectedItemName, driver.FindElement(By.XPath("//*[@id='layer_cart_product_title']")).Text);
        }
        public static void ClickProceedToCheckoutOnOverlay(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@id='layer_cart']/div[1]/div[2]/div[4]/a")).Click();
        }
        public static void SearchForItem(IWebDriver driver, string searchItemName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            driver.FindElement(By.Id("search_query_top")).Clear();
            driver.FindElement(By.Id("search_query_top")).SendKeys(searchItemName);
            driver.FindElement(By.Name("submit_search")).Click();
            wait.Until(ExpectedConditions.TitleContains("Search"));
        }
        public static string RetreiveItemName(IWebDriver driver)
        {
            return driver.FindElement(By.XPath("//*[@id='homefeatured']/li[1]/div/div[2]/h5/a")).Text;
        }
        public static string RetrieveItemPrice(IWebDriver driver)
        {
            return driver.FindElement(By.XPath("//*[@id='homefeatured']/li[1]/div/div[2]/div[1]/span")).Text;
        }

    }
}
