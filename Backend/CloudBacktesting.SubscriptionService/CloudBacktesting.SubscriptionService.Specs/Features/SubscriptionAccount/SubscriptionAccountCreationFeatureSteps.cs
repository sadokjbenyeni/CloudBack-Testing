using CloudBacktesting.Infra.Security;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;
using CloudBacktesting.SubscriptionService.Specs.Host;
using CloudBacktesting.SubscriptionService.WebAPI.Models.Account.Client.SubscriptionAccount;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TechTalk.SpecFlow;

namespace CloudBacktesting.SubscriptionService.Specs.Features.SubscriptionAccount
{
    [Binding]
    public class SubscriptionAccountCreationFeatureSteps
    {
        private readonly ScenarioContext context;

        public SubscriptionAccountCreationFeatureSteps(ScenarioContext injectedContext, HttpClient httpClient)
        {
            context = injectedContext;
            context.Set(httpClient);
        }

        [Given(@"the webapi is online")]
        public void GivenTheWebapiIsOnline()
        {
            //Assert.That(context.Get<HttpClient>(), Is.Not.Null);// ((HttpClient)context.Get<IServiceProvider>().GetService(typeof(HttpClient)));
        }


        //[When(@"morgan gets the subscription account list")]
        //public async Task WhenMorganGetsTheSubscriptionAccountList()
        //{
        //    var httpClient = context.Get<HttpClient>();
        //    var request = await httpClient.GetAsync("api/subscriptionaccount");
        //    var bodyString = await request.Content.ReadAsStringAsync();
        //    var models = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SubscriptionAccountReadModel>>(bodyString);
        //    context.Set(models.ToList(), "GetSubscriptionAccountList");
        //}

        [When(@"'(.*)' gets his subscription account")]
        public async Task WhenMorganGetsTheSubscriptionAccountFor(string customer)
        {
            var resultCommand = context.Get<HttpResponseMessage>("createSubscriptionCommandResult");
            Assert.That(resultCommand.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var content = await resultCommand.Content.ReadAsStringAsync();
            var identifierContainer = JsonConvert.DeserializeObject<SubscriptionAccountIdDto>(content);

            var identity = context.Get<UserIdentity>(customer);
            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(identity);

            var request = await httpClient.GetAsync($"api/SubscriptionAccount");
            var bodyString = await request.Content.ReadAsStringAsync();
            var customerReadModel = JsonConvert.DeserializeObject<SubscriptionAccountReadModelDto>(bodyString);

            context.Set(customerReadModel);
        }

        [Then(@"Creation of subscription account is successful")]
        public async Task ThenCreationOfSubscriptionAccountIsSuccessful()
        {
            var resultCommand = context.Get<HttpResponseMessage>("createSubscriptionCommandResult");
            Assert.That(resultCommand.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var content = await resultCommand.Content.ReadAsStringAsync();
            var subscriptionAccountId = JsonConvert.DeserializeObject<SubscriptionAccountIdDto>(content);

            Assert.That(resultCommand.IsSuccessStatusCode, Is.True, resultCommand.ReasonPhrase);
            Assert.That(subscriptionAccountId, Is.Not.Null);
            Assert.That(subscriptionAccountId.Id, Is.Not.Null.And.Not.Empty);
        }

        [Then(@"get request return '(.*)' subscription account description")]
        public void ThenGetRequestReturnSubscriptionAccountDescription(string customer)
        {
            var dtoModel = context.Get<SubscriptionAccountReadModelDto>();
            Assert.That(dtoModel, Is.Not.Null);
            Assert.That(dtoModel.Subscriber, Is.EqualTo(customer).Using((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase));
            Assert.That(dtoModel.CreationDate.Date, Is.EqualTo(DateTime.UtcNow.Date));
        }

    }
}
