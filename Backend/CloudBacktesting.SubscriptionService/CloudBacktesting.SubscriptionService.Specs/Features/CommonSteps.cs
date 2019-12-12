using CloudBacktesting.SubscriptionService.WebAPI.Models.Account.Client.SubscriptionAccount;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CloudBacktesting.SubscriptionService.Specs.Features
{
    [Binding]
    public class CommonSteps
    {
        private ScenarioContext context;

        public CommonSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"(.*) is authentificated")]
        public void GivenMorganIsAuthentificated(string user)
        {
            
        }


        [Given(@"'(.*)' subscription account has been created")]
        public Task GivenSubscriptionAccountHasBeenCreated(string customer)
        {
            return CreateNewSubscriptionAccountFor(customer);
        }


        [When(@"morgan sends the subscription account creation request for '(.*)'")]
        public Task WhenMorganSendsTheSubscriptionAccountCreationRequestForChang(string customer)
        {
            return CreateNewSubscriptionAccountFor(customer);
        }

        private async Task CreateNewSubscriptionAccountFor(string customer)
        {
            var httpContext = context.Get<HttpClient>();
            var customerCommand = new CreateSubscriptionAccountDto() { Subscriber = customer };
            context.Set(customerCommand, "creationSubscriptionAccountCommand");
            var content = new StringContent(JsonConvert.SerializeObject(customerCommand), Encoding.UTF8, "application/json");
            var result = await httpContext.PostAsync("api/subscriptionaccount", content);
            context.Set(result, "createSubscriptionCommandResult");
        }

    }
}
