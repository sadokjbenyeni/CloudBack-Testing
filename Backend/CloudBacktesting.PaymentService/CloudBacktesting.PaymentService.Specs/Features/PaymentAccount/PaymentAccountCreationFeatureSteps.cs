using System;
using TechTalk.SpecFlow;

namespace CloudBacktesting.PaymentService.Specs.Features.PaymentAccount
{
    [Binding]
    public class PaymentAccountCreationFeatureSteps
    {
        [Given(@"Morgan is authentificated")]
        public void GivenMorganIsAuthentificated()
        {
            
        }
        
        [Given(@"the webapi is online")]
        public void GivenTheWebapiIsOnline()
        {
            
        }
        
        [Given(@"'(.*)' is authentificated")]
        public void GivenIsAuthentificated(string p0)
        {
            
        }
        
        [Given(@"'(.*)' payment account has been created")]
        public void GivenPaymentAccountHasBeenCreated(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"morgan sends the payment account creation request for '(.*)'")]
        public void WhenMorganSendsThePaymentAccountCreationRequestFor(string client)
        {
            var url = "api/paymentaccount"; // POST 
            //var client = "clientIdentifier";
            
            // httpClient.PostAync(url, "{ 'client': 'Chang' } }";

        }
        
        [When(@"'(.*)' gets his payment account")]
        public void WhenGetsHisPaymentAccount(string client)
        {
            var url = "api/paymentaccount"; // GET
            // httpClient.GetAsync(url);
        }

        [Then(@"Creation of payment account is successful")]
        public void ThenCreationOfPaymentAccountIsSuccessful()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"get request return '(.*)' payment account description")]
        public void ThenGetRequestReturnPaymentAccountDescription(string p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
