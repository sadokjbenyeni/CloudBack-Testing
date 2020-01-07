using CloudBacktesting.Infra.Security;
using CloudBacktesting.PaymentService.Specs.Host;
using CloudBacktesting.PaymentService.WebAPI.Models;
using CloudBacktesting.PaymentService.WebAPI.Models.PaymentAccount;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CloudBacktesting.PaymentService.Specs.Features
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
                Additionals = new Dictionary<string, string>(),
            };
            context.Set(identity, user);
        }


        [Given(@"'(.*)' payment account has been created")]
        public Task GivenPaymentAccountHasBeenCreated(string customer)
        {
            return CreateNewPaymentAccountFor(customer);
        }

        [When(@"Morgan sends the payment account creation request for '(.*)'")]
        public Task WhenMorganSendsThePaymentAccountCreationRequestFor(string customer)
        {
            return CreateNewPaymentAccountFor(customer);
        }

        private async Task CreateNewPaymentAccountFor(string customer)
        {
            var adminUser = context.Get<UserIdentity>("Morgan");
            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(adminUser);
            var customerCommand = new CreatePaymentAccountDto() { Client = customer };
            context.Set(customerCommand, "creationPaymentAccountCommand");
            var content = new StringContent(JsonConvert.SerializeObject(customerCommand), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync("api/paymentaccount", content);
            context.Set(result, "createPaymentCommandResult");
            if (result.IsSuccessStatusCode && context.TryGetValue(customer, out UserIdentity customerIdentity))
            {
                var id = JsonConvert.DeserializeObject<IdentifierDto>(await result.Content.ReadAsStringAsync());
                ((Dictionary<string, string>)customerIdentity.Additionals).Add("subscriptionaccountid", id.Id);
            }
        }

    }
}
