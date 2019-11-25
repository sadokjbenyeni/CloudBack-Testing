using System;
using TechTalk.SpecFlow;

namespace CloudBacktesting.SubscriptionService.Specs.Features.SubscriptionAccount
{
    [Binding]
    public class SubscriptionAccountCreationFeatureSteps
    {
        private readonly ScenarioContext context;

        public SubscriptionAccountCreationFeatureSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"Morgan is authentificated")]
        public void GivenMorganIsAuthentificated()
        {
            context.Pending();
        }
        
        [Given(@"the webapi is online")]
        public void GivenTheWebapiIsOnline()
        {
            context.Pending();
        }
        
        [Given(@"'(.*)' subscription account has been created")]
        public void GivenSubscriptionAccountHasBeenCreated(string p0)
        {
            context.Pending();
        }
        
        [When(@"morgan sends the subscription account creation request for Chang")]
        public void WhenMorganSendsTheSubscriptionAccountCreationRequestForChang()
        {
            context.Pending();
        }
        
        [When(@"morgan gets the subscription account list")]
        public void WhenMorganGetsTheSubscriptionAccountList()
        {
            context.Pending();
        }
        
        [Then(@"SubscriptionAccount entity has been created on the system")]
        public void ThenSubscriptionAccountEntityHasBeenCreatedOnTheSystem()
        {
            context.Pending();
        }
        
        [Then(@"get request return '(.*)' subscription account description")]
        public void ThenGetRequestReturnSubscriptionAccountDescription(string p0)
        {
            context.Pending();
        }
    }
}
