using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MailPoetAutomation.PageObjects
{
    static class CheckOutPage
    {
        public static void ConfirmItemDetails(IWebDriver driver, string selectedItemName, string selectedItemPrice)
        {
            //Confirm the correct item was added to cart.//*[@id="product_7_34_0_0"]/td[2]/p/a
            Assert.AreEqual(selectedItemName, driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div/div[2]/table/tbody/tr/td[2]/p/a")).Text);
            //Confirm the correct price is displayed for item added to cart.
            Assert.AreEqual(selectedItemPrice, driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div/div[2]/table/tbody/tr/td[4]/span/span[1]")).Text);
        }
        public static void ClickProceedToCheckOut(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@id='center_column']/p[2]/a[1]")).Click();
        }
        public static void ConfirmCartPrices(IWebDriver driver, string selectedItemPrice)
        {
            //Convert item price to double and remove $ sign and replace , with .
            selectedItemPrice = selectedItemPrice.Replace("$", "");
            selectedItemPrice = selectedItemPrice.Replace(".", ",");
            decimal itemPrice = decimal.Parse(selectedItemPrice);

            //Retrieve and convert quantity then multiply item price by quanity and convert back to string.
            int quanity = Int32.Parse(driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div/div[2]/table/tbody/tr/td[5]/input[2]")).GetAttribute("value"));
            decimal totalQtyPrice = itemPrice * quanity;
            string totalQtyPriceText = "$" + totalQtyPrice.ToString();
            totalQtyPriceText = totalQtyPriceText.Replace(",", ".");

            //Confirm the total quanity price is correct.
            Assert.AreEqual(totalQtyPriceText, driver.FindElement(By.Id("total_product")).Text);

            //Retrieve and Convert shipping to double and remove $ sign and replace , with .
            string shippingPriceText = driver.FindElement(By.Id("total_shipping")).Text;
            shippingPriceText = shippingPriceText.Replace("$", "");
            shippingPriceText = shippingPriceText.Replace(".", ",");
            decimal shippingPrice = decimal.Parse(shippingPriceText);

            //Add total Item price and shipping price and convert back to the string format $00.00
            decimal totalPriceWithoutTax = totalQtyPrice + shippingPrice;
            string totalPriceWithoutTaxText = "$" + totalPriceWithoutTax.ToString();
            totalPriceWithoutTaxText = totalPriceWithoutTaxText.Replace(",", ".");

            //Confirm that item price + shipping price is equal to total price without tax displayed.
            Assert.AreEqual(totalPriceWithoutTaxText, driver.FindElement(By.Id("total_price_without_tax")).Text);

            //Retrieve and Convert tax price to double and remove $ sign and replace , with .
            string taxPriceText = driver.FindElement(By.Id("total_tax")).Text;
            taxPriceText = taxPriceText.Replace("$", "");
            taxPriceText = taxPriceText.Replace(".", ",");
            decimal taxPrice = decimal.Parse(taxPriceText);

            //Add Total Item price,shipping price and tax then convert back to the string format $00.00
            decimal totalPrice = totalQtyPrice + shippingPrice + taxPrice;
            string totalPriceText = "$" + totalPrice.ToString();
            totalPriceText = totalPriceText.Replace(",", ".");

            //Confirm that item price + shipping price is equal to total price displayed.
            Assert.AreEqual(totalPriceText, driver.FindElement(By.Id("total_price")).Text);

        }
        public static void IncreaseQuantity(IWebDriver driver)
        {
            driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div/div[2]/table/tbody/tr/td[5]/div/a[2]/span/i")).Click();
        }
    }
}
