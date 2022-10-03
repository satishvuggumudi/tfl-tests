using Infrastructure.Entity;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Infrastructure.PageObjects
{
    public class PlanMyJourneyPage : BaseClass
    {
        public PlanMyJourneyPage(IWebDriver driver) : base(driver) { }

        
        [FindsBy(How = How.XPath, Using = "//a[@class='change-departure-time']")]
        private  readonly IWebElement ChangeTimeLink;

        [FindsBy(How = How.XPath, Using = "//label[text()='Arriving']")]
        private  readonly IWebElement ArrivingTime;

        [FindsBy(How = How.Id, Using = "Time")]
        private  readonly IWebElement Time;

        [FindsBy(How = How.XPath, Using = "//span[@class='field-validation-error']")]
        private readonly IWebElement ValidationErrors;

        [FindsBy(How = How.Id, Using = "jp-recent-tab-home")]
        private readonly IWebElement RecentTab;

        public void ClickOnChangeTime() => ChangeTimeLink.Click();

        public void ClickOnArravingTime() => ArrivingTime.Click();

        public void SelectTime(string time) => Time.SelectTextFromDropDown(time);

       


        public List<string> FormValidationErrors()
        {
            var findElements = Driver.FindElements(By.XPath("//span[@class='field-validation-error']"));
            var errors = new List<string>();

            foreach(var element in findElements)
            {
                element.GetText();
                errors.Add(element.Text);
            }
            return errors;
        }

        public List<string> RecentJourneyPlanResultsText()
        {
            var findElements = Driver.FindElements(By.XPath("//div[@id='jp-recent-content-home-']/a[@class='plain-button journey-item']"));

            var text = new List<string>();
            foreach (var element in findElements)
            {
                element.GetText();
                text.Add(element.Text);
            }
            return text;
        }

        public void ClickOnRecentTab() => RecentTab.Click();

    }

}
