using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest.Pages
{
    public class BasePage
    {
        public static IWebDriver driver;

        [OneTimeSetUp]
        public void Initialize()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--start-maximized");
            driver = new ChromeDriver(chromeOptions);
            string URL = "https://wahoofitness.com/";
            driver.Navigate().GoToUrl(URL);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 15));
            
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
