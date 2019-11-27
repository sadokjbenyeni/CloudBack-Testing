using System;
using TechTalk.SpecFlow;

namespace CloudBacktesting.SubscriptionService.Specs.Features.SubscriptionRequest
{
    [Binding]
    public class SubscriptionRequestFeatureSteps
    {
        [Given(@"'(.*)' subscription has been created for '(.*)'")]
        public void GivenSubscriptionHasBeenCreatedFor(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"Chang sends new request of subscription for Mutualized service")]
        public void WhenChangSendsNewRequestOfSubscriptionForMutualizedService()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"'(.*)' sends GET request to list these subscription")]
        public void WhenSendsGETRequestToListTheseSubscription(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"'(.*)' sends GET request with '(.*)' subscription")]
        public void WhenSendsGETRequestWithSubscription(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Creation of subscription is successful")]
        public void ThenCreationOfSubscriptionIsSuccessful()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"all subscription has been return at '(.*)'")]
        public void ThenAllSubscriptionHasBeenReturnAt(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"only '(.*)' subscription has been return at '(.*)'")]
        public void ThenOnlySubscriptionHasBeenReturnAt(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
