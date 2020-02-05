using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace CloudBacktesting.ApiGateway.Specs.Routing.NotSecure
{
    [Binding]
    public class NotSecureRoutingSteps
    {
        private readonly ScenarioContext context;

        public NotSecureRoutingSteps(ScenarioContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [Given(@"Changs is a new user on the web site")]
        public void GivenChangsIsANewUserOnTheWebSite()
        {
            context.Pending();
        }
        
        [Given(@"Chang filled all information on the sign up form")]
        public void GivenChangFilledAllInformationOnTheSignUpForm()
        {
            context.Pending();
        }
        
        [Given(@"API Version is (.*)")]
        public void GivenAPIVersionIs(int apiVersion)
        {
            context.Pending();
        }
        
        [Given(@"Changs a new customers on the web site")]
        public void GivenChangsANewCustomersOnTheWebSite()
        {
            context.Pending();
        }
        
        [Given(@"Chang is filling information on the form")]
        public void GivenChangIsFillingInformationOnTheForm()
        {
            context.Pending();
        }
        
        [When(@"Chang click on submit")]
        public void WhenChangClickOnSubmit()
        {
            context.Pending();
        }
        
        [When(@"Api gateway routes the source '(.*)' request from '(.*)' to destination '(.*)'")]
        public void WhenApiGatewayRoutesTheSourceRequestFromToDestination(string httpVerb, string sourceUrl, string destinationUrl)
        {
            context.Pending();
        }
        
        [When(@"Changs click to change set his countries")]
        public void WhenChangsClickToChangeSetHisCountries()
        {
            context.Pending();
        }
        
        [When(@"Web site sends query at the server to list all countries supported")]
        public void WhenWebSiteSendsQueryAtTheServerToListAllCountriesSupported()
        {
            context.Pending();
        }
        
        [Then(@"the api gateway routes the '(.*)' request from '(.*)' to '(.*)'")]
        public void ThenTheApiGatewayRoutesTheRequestFromTo(string httpVerb, string sourceUrl, string destinationUrl)
        {
            context.Pending();
        }
        
        [Then(@"all information include in the source request are in the destination request")]
        public void ThenAllInformationIncludeInTheSourceRequestAreInTheDestinationRequest()
        {
            context.Pending();
        }
        
        [Then(@"api gateway routes the query from '(.*)' to '(.*)'")]
        public void ThenApiGatewayRoutesTheQueryFromTo(string sourceUrl, string destinationUrl)
        {
            context.Pending();
        }
        
        [Then(@"the HTTP Header has been preserved")]
        public void ThenTheHTTPHeaderHasBeenPreserved()
        {
            context.Pending();
        }
    }
}
