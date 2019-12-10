using CloudBacktesting.SubscriptionService.WebAPI.Models.Account.Client.SubscriptionAccount;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CloudBacktesting.SubscriptionService.Specs.Features.SubscriptionRequest
{
    [Binding]
    public class SubscriptionValidationRequestFeatureSteps
    {
        private readonly ScenarioContext context;

        public SubscriptionValidationRequestFeatureSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"populates repositry with subscription account and requests")]
        public Task GivenPopulatesRepositryWithSubscriptionAccountAndRequests()
        {
            var commonSteps = new CommonSteps(context);
            var list = new List<string>();
            context.Set(list, "expectedSubscriptionAccountUserName");
            return Task.WhenAll(Enumerable.Range(0, 20)
                .Select(index =>
                {
                    var user = $"customer-{index}";
                    list.Add(user);
                    return commonSteps.GivenSubscriptionAccountHasBeenCreated(user);
                }));
        }
        
        [When(@"'(.*)' sends GET request on subscription request which are being created")]
        public async Task WhenSendsGETRequestOnSubscriptionRequestWhichAreBeingCreated(string adminUser)
        {
            var httpContext = context.Get<HttpClient>();
            var result = await httpContext.GetAsync("api/adminsubscription");
            context.Set(result, "getAllSubscriptionAccount");
        }
        
        [Then(@"all subscription request has been return")]
        public async Task ThenAllSubscriptionRequestHasBeenReturn()
        {
            var expected = context.Get<List<string>>("expectedSubscriptionAccountUserName");
            var actual = await context.Get<HttpResponseMessage>("getAllSubscriptionAccount").Content.ReadAsStringAsync()
                                .ContinueWith(task => JsonConvert.DeserializeObject<IEnumerable<SubscriptionAccountReadModelDto>>(task.Result));
            Assert.That(actual.Select(dto => dto.Subscriber).Where(subscriber => !string.Equals("chang", subscriber, StringComparison.CurrentCultureIgnoreCase)).ToList(), Is.EquivalentTo(expected));
        }

        [When(@"'(.*)' sends GET admin request with '(.*)' subscription")]
        public void WhenSendsGETAdminRequestWithSubscription(string p0, string p1)
        {
            //var httpContext = context.Get<HttpClient>();
            //var result = await httpContext.GetAsync("api/adminsubscription");
            //context.Set(result, "getAllSubscriptionAccount");
        }

    }
}
