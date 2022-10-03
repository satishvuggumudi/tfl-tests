using Infrastructure;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Tests
{
    [Binding]
    public class LifeCycle
    {
        [BeforeScenario()]
        public static void Setup(FeatureContext featureContext)
        {
            var webDriver = new Driver(ApplicationConfigBuilder.Instance.BrowserType);
            webDriver.Browser.GoTo(ApplicationConfigBuilder.Instance.Url);
            webDriver.Browser.WaitForPageToLoad();
            webDriver.Browser.AccecptCookies();            
            featureContext[Constants.Browser] = webDriver.Browser;
        }

        [AfterScenario()]
        public static void Teardown(FeatureContext featureContext)
        {
            var webDriver = (IWebDriver) featureContext[Constants.Browser];
            webDriver.Quit();
        }

    }
}