using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumTest.Pages;

namespace SeleniumTest
{
    public class Tests : BasePage
    {
        WahooPage wahooPage;

        [SetUp]
        public void SetUp()
        {
            wahooPage = new WahooPage(driver);
        }

        [Test]
        public void OpenShop()
        {
            wahooPage.CloseCookie();
            wahooPage.Products.Click();
            wahooPage.AddToCart();
            driver.Navigate().Back();
            wahooPage.AddToCart();
            wahooPage.RemoveFromCart();
            wahooPage.EditCart();
            wahooPage.CheckoutCart();
            Assert.AreEqual(driver.Title,"Alive");
        }

        [Test]
        public void Select()
        {
            wahooPage.CloseCookie();
            wahooPage.AddToCart();
            Assert.AreEqual(driver.Title, "Alive");
        }
        
    }
}