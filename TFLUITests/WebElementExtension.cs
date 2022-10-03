using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Infrastructure
{
    public static class WebElementExtension
    {

        public static void SendText( this IWebElement element, string value )
        {
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
            element.SendKeys(value);
            element.SendKeys(Keys.Tab);
        }

        public static void SelectTextFromDropDown(this IWebElement element, string value)
        {
            var selectElement = new SelectElement(element);
                selectElement.SelectByText(value);
        }

        public static string GetText(this IWebElement element) => element.Text;

        public static bool ElementDisplayed(this IWebElement element) => element.Displayed;

    }
}
