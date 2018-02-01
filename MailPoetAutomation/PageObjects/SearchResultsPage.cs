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
    static class SearchResultsPage
    {
        public static void ConfirmSearchedItem(IWebDriver driver, string searchedItemName)
        {
            Assert.AreEqual(searchedItemName, driver.FindElement(By.XPath("//*[@id='center_column']/ul/li[1]/div/div[2]/h5/a")).Text);
        }
        public static void SelectSearchedItem(IWebDriver driver, string searchedItemName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            //Perform mouse hover and click to add item to cart.
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.XPath("//*[@id='center_column']/ul/li[1]/div/div[2]/h5/a"))).Click(driver.FindElement(By.XPath("//*[@id='center_column']/ul/li[1]/div/div[2]/div[2]/a[2]"))).Build().Perform();
            wait.Until(ExpectedConditions.TitleContains(searchedItemName));
        }
        public static string RetrieveSearchItemPrice(IWebDriver driver)
        {
            return driver.FindElement(By.XPath("//*[@id='center_column']/ul/li[1]/div/div[2]/div[1]/span[1]")).Text;
        }
    }
}
