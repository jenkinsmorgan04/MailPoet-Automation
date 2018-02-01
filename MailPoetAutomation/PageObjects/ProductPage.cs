using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPoetAutomation.PageObjects
{
    static class ProductPage
    {
        public static void ConfirmSelectedItemDetails(IWebDriver driver, string selectedItemName, string selectedItemPrice)
        {
            //Confirm the correct item is being viewed.
            Assert.AreEqual(selectedItemName, driver.FindElement(By.XPath("//*[@id='center_column']/div/div/div[3]/h1")).Text);
            //Confirm the correct price is displayed for item being viewed.
            Assert.AreEqual(selectedItemPrice, driver.FindElement(By.Id("our_price_display")).Text);
        }
        public static void IncreaseQuantity(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@id='quantity_wanted_p']/a[2]")).Click();
        }
        public static void AddToCart(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@id='add_to_cart']/button")).Click();
        }
    }
}
