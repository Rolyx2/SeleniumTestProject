using Microsoft.VisualBasic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using SeleniumTest.Pages;
using System;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace SeleniumTest.Pages
{
    public class WahooPage : BasePage
    {
        public WahooPage(IWebDriver driver)
        {
            WahooPage.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 15));

        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'onetrust-close-btn-handler') and @aria-label='Close']")]
        public IWebElement CloseCookieButton;

         public void CloseCookie()
        {
            wait.Until(driver => CloseCookieButton.Displayed);
            CloseCookieButton.Click();
        }

        public void JavaScriptClick(IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        [FindsBy(How = How.XPath, Using = "//a[@title='Ride']")]
        public IWebElement Products;

        [FindsBy(How = How.XPath, Using = "//button[@id='product-addtocart-button']")]
        public IWebElement AddToCartButton;

        [FindsBy(How = How.XPath, Using = "//button[@id='top-cart-btn-checkout']")]
        public IWebElement CheckoutButton;

        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'action-accept')]")]
        public IWebElement AcceptButton;

        [FindsBy(How = How.XPath, Using = "//a[@class='action viewcart']")]
        public IWebElement ViewCart;

        [FindsBy(How = How.XPath, Using = "//button[@class='action update']")]
        public IWebElement UpdateCart;

        [FindsBy(How = How.XPath, Using = "//input[@class='input-text qty']")]
        public IWebElement CountInput;

        [FindsBy(How = How.XPath, Using = "//td[@data-th=\"Order Total\"]//span[@class='price']")]
        public IWebElement PriceValue;

        [FindsBy(How = How.XPath, Using = "//button[@data-role='proceed-to-checkout']")]
        public IWebElement CheckoutFromCartButton;

        [FindsBy(How = How.XPath, Using = "//div[@class='payment-methods']//button[@title='Pay Now']")]
        public IWebElement PayNowButton;

        [FindsBy(How = How.XPath, Using = "//div[@style='display: none;' and @class='loading-mask']")]
        public IWebElement Loader;

        [FindsBy(How = How.XPath, Using = "//div[@id='customer-email-error']")]
        public IWebElement EmailErrorMessage;

        [FindsBy(How = How.XPath, Using = "//tr[contains(.,'Express Shipping')]//input")]
        public IWebElement ExpresShippingButton;

        [FindsBy(How = How.XPath, Using = "//tr[@class='totals shipping excl']//span[contains(.,'Express Shipping')]")]
        public IWebElement ExpresShippingTotalMethod;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'email')]")]
        public IWebElement EmailField;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'email')]")]
        public IWebElement FirstNameField;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'email')]")]
        public IWebElement SecondNameField;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'email')]")]
        public IWebElement StreetAddressField;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'email')]")]
        public IWebElement CityField;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'email')]")]
        public IWebElement CountryField;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'email')]")]
        public IWebElement StateField;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'email')]")]
        public IWebElement ZipField;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'email')]")]
        public IWebElement PhoneNumberField;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'email')]")]
        public IWebElement CardField;

        public void SelectRandomProduct()
        {
            IList<IWebElement> productElems = driver.FindElements(By.XPath("//a[contains(@class,'product-image')]"));
            int maxProducts = productElems.Count();
            Random random = new Random();
            int randomProductNumber = random.Next(maxProducts);
            productElems[randomProductNumber].Click();
        }

        public bool CheckItem()
        {
            IList<IWebElement> productElems = driver.FindElements(By.XPath("//select"));
            foreach (IWebElement productElem in productElems)
            {
                try
                {
                    var selectElement = new SelectElement(productElem);
                    var firstSelectedElement = selectElement.Options.Where(oo => oo.Enabled && !oo.Text.Contains("Choose an Option")).First();
                    selectElement.SelectByIndex(selectElement.Options.IndexOf(firstSelectedElement));
                } catch (Exception)
                {

                }
            }

            try
            {
                wait.Until(driver => AddToCartButton.Displayed);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public void AddToCart()
        {
            bool Succsess = false;
            while (!Succsess)
            {
                SelectRandomProduct();
                Succsess = CheckItem();
                if (!Succsess)
                {
                    driver.Navigate().Back();
                }
            }
            AddToCartButton.Click();
            wait.Until(driver => CheckoutButton.Displayed);

            //https://eu.wahoofitness.com/devices/accessories/apparel/wahoo-fitness-cycling-cap cancer example
        }

        public void RemoveFromCart()
        {
            IList<IWebElement> productElems = driver.FindElements(By.XPath("//a[@class='action delete']"));
            int maxProducts = productElems.Count();
            productElems.First().Click();
            wait.Until(driver => AcceptButton.Displayed);
            AcceptButton.Click();
            wait.Until(driver => driver.FindElements(By.XPath("//a[@class='action delete']")).Count == 1);
            
        }

        public void EditCart()
        {
            wait.Until(driver => ViewCart.Displayed);
            ViewCart.Click();
            wait.Until(driver => UpdateCart.Displayed);

            string firstValue = PriceValue.GetAttribute("textContent");
            CountInput.Clear();
            CountInput.SendKeys("2");
            UpdateCart.Click();
            wait.Until(driver => firstValue != PriceValue.GetAttribute("textContent"));
        }

        public void CheckoutCart()
        {
            CheckoutFromCartButton.Click();
            wait.Until(driver => PayNowButton.Displayed);
            wait.Until(driver => !Loader.Displayed);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='payment-methods']//button[@title='Pay Now']")));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", PayNowButton);
            wait.Until(driver => EmailErrorMessage.Displayed);
            ExpresShippingButton.Click();
            wait.Until(driver => ExpresShippingTotalMethod.Displayed);
        }
        public void ShipInfo(string email, string name, string address, string phone, string creditCard)
        {

        }
    }   
}
