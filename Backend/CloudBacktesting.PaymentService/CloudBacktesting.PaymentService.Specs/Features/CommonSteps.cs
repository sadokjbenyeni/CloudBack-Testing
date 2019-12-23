using CloudBacktesting.PaymentService.WebAPI.Models.PaymentAccount;
using Newtonsoft.Json;
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

        [Given(@"Morgan is authentificated")]
        public void GivenMorganIsAuthentificated()
        {

        }

        [Given(@"'(.*)' is authentificated")]
        public void GivenMorganIsAuthentificated(string user)
        {

        }

        [Given(@"'(.*)' payment account has been created")]
        public Task GivenPaymentAccountHasBeenCreated(string customer)
        {
            return CreateNewPaymentAccountFor(customer);
        }

        [When(@"morgan sends the payment account creation request for '(.*)'")]
        public Task WhenMorganSendsThePaymentAccountCreationRequestFor(string customer)
        {
            return CreateNewPaymentAccountFor(customer);
        }

        private async Task CreateNewPaymentAccountFor(string customer)
        {
            var httpContext = context.Get<HttpClient>();
            var customerCommand = new CreatePaymentAccountDto() { Client = customer };
            context.Set(customerCommand, "creationPaymentAccountCommand");
            var content = new StringContent(JsonConvert.SerializeObject(customerCommand), Encoding.UTF8, "application/json");
            var result = await httpContext.PostAsync("api/paymentaccount", content);
            context.Set(result, "createPaymentCommandResult");
        }
    }
}
