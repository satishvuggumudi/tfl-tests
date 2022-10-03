using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Infrastructure.PageObjects
{
    public class JouneyDetailsPage : BaseClass
    {
        public JouneyDetailsPage(IWebDriver driver) : base(driver) { }

        [FindsBy(How = How.CssSelector, Using = "span.jp-results-headline")]
        private readonly IWebElement JourneyDetailsPageHeader;

        [FindsBy(How = How.XPath, Using = "//li[@class='field-validation-error']")]
        private readonly IWebElement FieldValidationError;  

        [FindsBy(How = How.XPath, Using = "//div[@class='extra-journey-options multi-modals clearfix']/h2")]
        private readonly IWebElement OtherOptionsHeader;

        [FindsBy(How = How.XPath, Using = "//div[@class='journey-result-summary')]")]
        private readonly IWebElement SummaryResults;

        [FindsBy(How = How.XPath, Using = "//div[@class='journey-results publictransport no-map']")]
        private readonly IWebElement JourneyResults;


        [FindsBy(How = How.XPath, Using = "//div[@class='journey-result-summary']//span[text()='To:']/parent::div//strong")]
        private readonly IWebElement ToLocationText;

        [FindsBy(How = How.XPath, Using = "//div[@class='journey-result-summary']//span[text()='From:']/parent::div//strong")]
        private readonly IWebElement FromLocationText;

        [FindsBy(How = How.XPath, Using = "//div[@class='journey-result-summary']//span[text()='Arriving:']/parent::div//strong")]
        private readonly IWebElement ArrivingTimeText;

        [FindsBy(How = How.CssSelector, Using = "a.edit-journey > span")]
        private readonly IWebElement EditJourney;

        [FindsBy(How = How.XPath, Using = "//div[@class='journey-planner-results']//span[text()='Home']")]
        private readonly IWebElement HomeButton;

        [FindsBy(How = How.Id, Using = "option-2-heading")]
        private readonly IWebElement FirstOption;       


        public string GetHeaderText() => JourneyDetailsPageHeader.GetText();
        public string GetSummaryText() => SummaryResults.GetText();

        public string GetToLocationText() => ToLocationText.GetText();
        public string GetFromLocationText() => FromLocationText.GetText();
        public string GetArrivingText() => ArrivingTimeText.GetText();

        public void ClickOnEditJourney() => EditJourney.Click();

        public string GetOtherHeaderOptionsText() => OtherOptionsHeader.GetText();
        public string GetFieldValidationErrorText() => FieldValidationError.GetText();

        public bool GetJourneyResultsText() => JourneyResults.ElementDisplayed();

        public void ClickOnHomeButton() => HomeButton.Click();

        public void ClickOnFirstOptionFromResults() => FirstOption.Click();

    }
}
