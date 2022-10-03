using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;

namespace Infrastructure
{
    public class Driver
    {
        public static bool HeadlessMode { get; private set; }
        public Driver(BrowserTypes browserType) => Use(browserType);

        public IWebDriver Browser { get; private set; }



        private void Use(BrowserTypes browser)
        {
            switch (browser)
            {
                case BrowserTypes.Chrome:
                    {
                        Browser = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
                        Browser.Manage().Window.Size = new Size(1980, 1080);
                        break;
                    }
                case BrowserTypes.HeadLessChrome:
                    {
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments("headless");
                        chromeOptions.AddArgument("--disable-gpu");
                        chromeOptions.AddArguments("--lang=en-US");
                        chromeOptions.AddArguments("window-size=1920,1080");
                        Browser = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, chromeOptions);
                        HeadlessMode = true;
                        break;
                    }

                default:
                    {
                        Browser = new ChromeDriver();
                        break;
                    }
            }

            Browser.Manage().Window.Maximize();

            Browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
    }

    public enum BrowserTypes
    {
        Chrome,
        HeadLessChrome
    }
}
