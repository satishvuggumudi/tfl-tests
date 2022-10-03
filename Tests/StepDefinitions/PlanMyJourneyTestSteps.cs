using FluentAssertions;
using Infrastructure;
using Infrastructure.Entity;
using Infrastructure.PageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Tests.StepDefinitions
{
    [Binding]
    public class PlanMyJourneyTestSteps
    {
        private readonly FeatureContext _featureContext;

        
        private  PlanMyJourneyPage _planMyJourneyPage;
        private JouneyDetailsPage _jouneyDetailsPage;

        public PlanMyJourneyTestSteps(FeatureContext featureContext)
        {
            var driver = (IWebDriver)featureContext[Constants.Browser];
            _planMyJourneyPage = new PlanMyJourneyPage(driver);
            _jouneyDetailsPage = new JouneyDetailsPage(driver);
            _featureContext = featureContext;
        }

        [Given(@"I enter locations")]
        public void GivenIEnterLocations(Table table)
        {
            var locations = table.CreateInstance<PlanMyJourneyEntity>();

            _planMyJourneyPage.InputLocations(locations);
            _featureContext[StepConstants.ToLocation] = locations.ToLocation;
            _featureContext[StepConstants.FromLocation] = locations.FromLocation;
                       
        }

        [StepDefinition(@"I Edit the journey location details")]
        public void GivenIEditTheJourneyLocationDetails(Table table)
        {
            var locations = table.CreateInstance<PlanMyJourneyEntity>();

            _jouneyDetailsPage.InputLocations(locations);
            _featureContext[StepConstants.ToLocation] = locations.ToLocation;
            _featureContext[StepConstants.FromLocation] = locations.FromLocation;
        }


        [Given(@"I update Change time options")]
        public void GivenIUpdateChangeTimeOptions(Table table)
        {
            var changeTimeOptions = table.CreateInstance<PlanMyJourneyEntity>();

            _planMyJourneyPage.ClickOnChangeTime();
            if (changeTimeOptions.Option == "Arriving")
            {
                _planMyJourneyPage.ClickOnArravingTime();
            }
            _planMyJourneyPage.SelectTime(changeTimeOptions.Time);
            _featureContext[StepConstants.ArrivingTime] = changeTimeOptions.Time;
        }

        [When(@"I click on plan a journey reference link")]
        public void WhenIClickOnPlanAJourneyReferenceLink()
        {
            _jouneyDetailsPage.ClickOnHomeButton();
        }

        [When(@"I click on a result")]
        public void WhenIClickOnAResult()
        {
            _jouneyDetailsPage.ClickOnFirstOptionFromResults();
        }


        [When(@"click on recent journey tab")]
        public void WhenClickOnRecentJourneyTab()
        {
            _planMyJourneyPage.ClickOnRecentTab();
        }



        [When(@"I click on plan my journey button")]
        public void WhenIClickOnPlanMyJourneyButton()
        {
            _planMyJourneyPage.ClickOnPlanMyJourneyButton();
        }


        [When(@"I click on update my journey button")]
        public void WhenIClickOnUpdateMyJourneyButton()
        {
            _jouneyDetailsPage.ClickOnPlanMyJourneyButton();
        }

        [When(@"I click on edit journey button")]
        public void WhenIClickOnEditJourneyButton()
        {
            _jouneyDetailsPage.ClickOnEditJourney();
        }



        [Then(@"I validate my journey results")]
        public void ThenIValidateMyJourneyResults()
        {
            _jouneyDetailsPage.GetHeaderText().Should().BeEquivalentTo("Journey results");
            _jouneyDetailsPage.GetToLocationText().Should().BeEquivalentTo(_featureContext[StepConstants.ToLocation].ToString());
            _jouneyDetailsPage.GetFromLocationText().Should().BeEquivalentTo(_featureContext[StepConstants.FromLocation].ToString());

            _jouneyDetailsPage.GetJourneyResultsText().Should().BeTrue();
        }

        [Then(@"I should see fieldvalidation errors")]
        public void ThenIShouldSeeFieldvalidationErrors(Table table)
        {
            var expectedErrors = table.Rows.Select(r => r["Error"]);
            var actualErrors = _planMyJourneyPage.FormValidationErrors();
            actualErrors.Should().Contain(expectedErrors);

        }

        [Then(@"I should see a field validation error")]
        public void ThenIShouldSeeAFieldValidationError(Table table)
        {
            var error = table.Rows.Select(r => r["Error"]);
            _jouneyDetailsPage.GetHeaderText().Should().BeEquivalentTo("Journey results");
            _jouneyDetailsPage.GetFieldValidationErrorText().Should().Contain(error.FirstOrDefault());

        }

        [Then(@"I validate my updated journey results")]
        public void ThenIValidateMyUpdatedJourneyResults()
        {
            _jouneyDetailsPage.GetHeaderText().Should().BeEquivalentTo("Journey results");
            _jouneyDetailsPage.GetToLocationText().Should().BeEquivalentTo(_featureContext[StepConstants.ToLocation].ToString());
            _jouneyDetailsPage.GetFromLocationText().Should().BeEquivalentTo(_featureContext[StepConstants.FromLocation].ToString());

            // Just checking for Time selected is displaying
            _jouneyDetailsPage.GetArrivingText().Should().Contain(_featureContext[StepConstants.ArrivingTime].ToString());

            _jouneyDetailsPage.GetJourneyResultsText().Should().BeTrue();
        }


        [Then(@"I should be able to see the recent jouneys")]
        public void ThenIShouldBeAbleToSeeTheRecentJouneys()
        {
            var actualResults = _planMyJourneyPage.RecentJourneyPlanResultsText();

            

            var expectedResult = _featureContext[StepConstants.FromLocation].ToString() + " to " + _featureContext[StepConstants.ToLocation].ToString();

            actualResults.Should().Contain(expectedResult);
        }

    }
}
