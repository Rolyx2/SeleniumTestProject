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
        string email;
        string firstName;
        string lastName;
        string address;
        string city;
        int zipNumber;
        long phoneNumber;
        long creditCardNumber;
        string creditCardDate;
        int creditCardCvc;

        [SetUp]
        public void SetUp()
        {
            wahooPage = new WahooPage(driver);
            email = "someone@whocares.com";
            firstName = "Test";
            lastName = "Test";
            address = "Comandante Izarduy 67, Barcelona";
            city = "Barcelona";
            zipNumber = 08940;
            phoneNumber = 5555555555;
            creditCardNumber = 4111111111111111;
            creditCardDate = "08/24";
            creditCardCvc = 111;
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
            wahooPage.ShipInfo(email,firstName,lastName,address, city, phoneNumber,zipNumber,creditCardNumber,creditCardDate, creditCardCvc);
            wahooPage.FinishOrder();
            Assert.IsTrue(wahooPage.CardErrorMessage.Displayed);
        }

        /*
        [Test]
        public void Select()
        {
            wahooPage.CloseCookie();
            wahooPage.AddToCart();
            Assert.AreEqual(driver.Title, "Alive");
        }
        */
    }
}