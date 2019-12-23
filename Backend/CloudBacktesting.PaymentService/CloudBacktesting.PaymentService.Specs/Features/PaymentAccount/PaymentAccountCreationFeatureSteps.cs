using CloudBacktesting.PaymentService.Domain.Repositories.PaymentAccountRepository;
using CloudBacktesting.PaymentService.WebAPI.Models.PaymentAccount;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TechTalk.SpecFlow;

namespace CloudBacktesting.PaymentService.Specs.Features.PaymentAccount
{
    [Binding]
    public class PaymentAccountCreationFeatureSteps
    {
        private readonly ScenarioContext context;

        public PaymentAccountCreationFeatureSteps(ScenarioContext injectedContext, HttpClient httpClient)
        {
            context = injectedContext;
            context.Set(httpClient);
        }
   
        [Given(@"the webapi is online")]
        public void GivenTheWebapiIsOnline()
        {
            Assert.That(context.Get<HttpClient>(), Is.Not.Null);// ((HttpClient)context.Get<IServiceProvider>().GetService(typeof(HttpClient)));

        }

        [When(@"morgan gets the payment account list")]
        public async Task WhenMorganGetsThePaymentAccountList()
        {
            var httpClient = context.Get<HttpClient>();
            var request = await httpClient.GetAsync("api/paymentaccount");
            var bodyString = await request.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<IEnumerable<PaymentAccountReadModel>>(bodyString);
            context.Set(models.ToList(), "GetPaymentAccountList");
        }

        [When(@"'(.*)' gets his payment account")]
        public async Task WhenGetsHisPaymentAccount(string customer)
        {
            var resultCommand = context.Get<HttpResponseMessage>("createPaymentCommandResult");
            Assert.That(resultCommand.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var content = await resultCommand.Content.ReadAsStringAsync();
            var identifierContainer = JsonConvert.DeserializeObject<PaymentAccountIdDto>(content);

            var httpClient = context.ScenarioContainer.Resolve<HttpClient>();
            var request = await httpClient.GetAsync($"api/PaymentAccount/{HttpUtility.UrlEncode(identifierContainer.Id)}");
            var bodyString = await request.Content.ReadAsStringAsync();
            var customerReadModel = JsonConvert.DeserializeObject<PaymentAccountReadModelDto>(bodyString);

            context.Set(customerReadModel);
        }

        [Then(@"Creation of payment account is successful")]
        public async Task ThenCreationOfPaymentAccountIsSuccessful()
        {
            var resultCommand = context.Get<HttpResponseMessage>("createPaymentCommandResult");
            Assert.That(resultCommand.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var content = await resultCommand.Content.ReadAsStringAsync();
            var identifierContainer = JsonConvert.DeserializeObject<PaymentAccountIdDto>(content);

            //var httpClient = context.ScenarioContainer.Resolve<HttpClient>();
            var httpClient = context.ScenarioContainer.Resolve<HttpClient>();
            var request = await httpClient.GetAsync($"api/PaymentAccount/{HttpUtility.UrlEncode(identifierContainer.Id)}");
            var bodyString = await request.Content.ReadAsStringAsync();
            var customerReadModel = JsonConvert.DeserializeObject<PaymentAccountReadModelDto>(bodyString);
            Assert.That(customerReadModel, Is.Not.Null);
            Assert.That(customerReadModel.Id, Is.EqualTo(identifierContainer.Id));
            Assert.That(customerReadModel.Client, Is.EqualTo(context.Get<CreatePaymentAccountDto>("creationPaymentAccountCommand").Client));
        }

        [Then(@"get request return '(.*)' payment account description")]
        public void ThenGetRequestReturnPaymentAccountDescription(string customer)
        {
            var dtoModel = context.Get<PaymentAccountReadModelDto>();
            Assert.That(dtoModel, Is.Not.Null);
            Assert.That(dtoModel.Client, Is.EqualTo(customer).Using((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase));
            Assert.That(dtoModel.CreationDate.Date, Is.EqualTo(DateTime.UtcNow.Date));
        }
    }
}
