using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;

namespace Infrastructure
{


    public static class WebDriverExtensions
    {
        

        public static void WaitForPageToLoad(this IWebDriver driver)
        {
            string DocumentState() => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString();

            while (DocumentState() != "complete")
            {
                Thread.Sleep(50);
            }

            bool ActiveAjaxCalls() => Convert.ToBoolean(((IJavaScriptExecutor)driver).ExecuteScript("return window.jQuery != undefined && jQuery.active > 0"));

            while (ActiveAjaxCalls())
            {
                Thread.Sleep(50);
            }

            Thread.Sleep(1000);
        }

        public static void AccecptCookies(this IWebDriver driver)
        {
            try
            {

                driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll")).Click();

                driver.FindElement(By.XPath("//h2[text()='Your cookie settings are saved']/parent::div/following-sibling::div/button/strong")).Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("No Cookies Enabled");
            }
        }


        public static IWebElement WaitForElement(this IWebDriver driver, By by, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(timeout),
                    TimeSpan.FromMilliseconds(50));
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
                return element;
            }
            catch (Exception e)
            {
                TakeScreenShot(driver, MethodBase.GetCurrentMethod().Name);
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static IWebElement Get(this IWebDriver driver, By by)
        {
            return driver.FindElement(@by);
        }
   
        public static void GoTo(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static void SubmitAndWait(this IWebDriver driver, IWebElement element)
        {
            try
            {
                element.Click();
                driver.WaitForPageToLoad();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                driver.TakeScreenShot(MethodBase.GetCurrentMethod().Name);
            }

        }

        public static bool CheckElementExists(this IWebDriver driver, string id)
        {
            try
            {
                driver.FindElement(By.Id(id));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public static void DefaultWait(this IWebDriver driver, int timeInSeconds = 3)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(timeInSeconds));
        }

        public static void TakeScreenShot(this IWebDriver driver, string methodName)
        {
            //((ITakesScreenshot)driver).GetScreenshot()
            //    .SaveAsFile(
            //        $"{ApplicationConfigurationBuilder.Instance.ScreenshotFilePath}/{methodName}_{Guid.NewGuid()}.png"
            //        , ScreenshotImageFormat.Png);
        }

    }
}
