using CloudBacktesting.Infra.Security;
using CloudBacktesting.SubscriptionService.Specs.Host;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Models.Account.Client.SubscriptionAccount;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Given(@"(.*) is authentificated with roles '(.*)'")]
        public void GivenMorganIsAuthentificated(string user, string roles)
        {
            var identity = new UserIdentity
            {
                Name = user,
                Roles = roles.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(role => role.Trim()).ToList(),
                Email = $"{user}@mail.com",
                Additionals = new Dictionary<string, string>()
                {
                    { "paymentaccountid", $"paymentaccount-{Guid.NewGuid().ToString()}"},
                },
            };
            context.Set(identity, user);
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
            var adminUser = context.Get<UserIdentity>("Morgan");
            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(adminUser);
            var customerCommand = new CreateSubscriptionAccountDto() { Subscriber = customer };
            context.Set(customerCommand, "creationSubscriptionAccountCommand");
            
            var content = new StringContent(JsonConvert.SerializeObject(customerCommand), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync("api/v1/subscriptionaccount", content);
            context.Set(result, "createSubscriptionCommandResult");
            if (result.IsSuccessStatusCode && context.TryGetValue(customer, out UserIdentity customerIdentity))
            {
                var id = JsonConvert.DeserializeObject<IdentifierDto>(await result.Content.ReadAsStringAsync());
                ((Dictionary<string, string>)customerIdentity.Additionals).Add("subscriptionaccountid", id.Id);
            }
        }

    }
}
