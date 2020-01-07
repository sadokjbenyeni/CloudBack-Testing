using CloudBacktesting.Infra.Security;
using CloudBacktesting.PaymentService.Domain.Repositories.PaymentAccountRepository;
using CloudBacktesting.PaymentService.Specs.Host;
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

        public PaymentAccountCreationFeatureSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }
   
        [Given(@"the webapi is online")]
        public void GivenTheWebapiIsOnline()
        {
        }

        [When(@"'(.*)' gets his payment account")]
        public async Task WhenGetsHisPaymentAccount(string customer)
        {
            var customerIdentity = context.Get<UserIdentity>(customer);
            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(customerIdentity);
            var request = await httpClient.GetAsync($"api/PaymentAccount");
            var bodyString = await request.Content.ReadAsStringAsync();
            var customerReadModel = JsonConvert.DeserializeObject<PaymentAccountReadModelDto>(bodyString);

            context.Set(customerReadModel);
        }

        [Then(@"Creation of payment account is successful")]
        public async Task ThenCreationOfPaymentAccountIsSuccessful()
        {
            var resultCommand = context.Get<HttpResponseMessage>("createPaymentCommandResult");
            Assert.That(resultCommand.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(resultCommand.IsSuccessStatusCode, Is.True, resultCommand.ReasonPhrase);
            var content = await resultCommand.Content.ReadAsStringAsync();
            var identifierContainer = JsonConvert.DeserializeObject<PaymentAccountIdDto>(content);
            Assert.That(identifierContainer, Is.Not.Null);
            Assert.That(identifierContainer.Id, Is.Not.Null.And.Not.Empty);
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
