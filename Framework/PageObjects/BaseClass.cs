using Infrastructure.Entity;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Infrastructure.PageObjects
{
    public class BaseClass
    {
        protected IWebDriver Driver;


        public BaseClass(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.Id, Using = "InputFrom")]
        private readonly IWebElement InputFromLocation;

        [FindsBy(How = How.Id, Using = "InputTo")]
        private readonly IWebElement InputToLocation;

        [FindsBy(How = How.Id, Using = "plan-journey-button")]
        private readonly IWebElement PlanMyJourneyButton;

        public void InputLocations(PlanMyJourneyEntity planJourney)
        {

            if (!string.IsNullOrEmpty(planJourney.FromLocation))
                InputFromLocation.SendText(planJourney.FromLocation);
            if (!string.IsNullOrEmpty(planJourney.ToLocation))
                InputToLocation.SendText(planJourney.ToLocation);
        }

        public void ClickOnPlanMyJourneyButton()
        {
            PlanMyJourneyButton.Click();
        }

        


    }
}
