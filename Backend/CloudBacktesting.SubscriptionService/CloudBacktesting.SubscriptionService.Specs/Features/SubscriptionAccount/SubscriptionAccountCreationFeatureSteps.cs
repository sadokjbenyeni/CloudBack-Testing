using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Models.SubscriptionAccount;
using EventFlow.Queries;
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
            Assert.That(context.Get<HttpClient>(), Is.Not.Null);// ((HttpClient)context.Get<IServiceProvider>().GetService(typeof(HttpClient)));
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

        [When(@"morgan gets the subscription account list")]
        public async Task WhenMorganGetsTheSubscriptionAccountList()
        {
            var httpClient = context.Get<HttpClient>();
            var request = await httpClient.GetAsync("api/subscriptionaccount");
            var bodyString = await request.Content.ReadAsStringAsync();
            var models = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SubscriptionAccountReadModel>>(bodyString);
            context.Set(models.ToList(), "GetSubscriptionAccountList");
        }

        [When(@"'(.*)' gets his subscription account")]
        public async Task WhenMorganGetsTheSubscriptionAccountFor(string customer)
        {
            var resultCommand = context.Get<HttpResponseMessage>("createSubscriptionCommandResult");
            Assert.That(resultCommand.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var content = await resultCommand.Content.ReadAsStringAsync();
            var identifierContainer = JsonConvert.DeserializeObject<SubscriptionAccountIdDto>(content);

            var httpClient = context.ScenarioContainer.Resolve<HttpClient>();
            var request = await httpClient.GetAsync($"api/SubscriptionAccount/{HttpUtility.UrlEncode(identifierContainer.Id)}");
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
            var identifierContainer = JsonConvert.DeserializeObject<SubscriptionAccountIdDto>(content);

            //var httpClient = context.ScenarioContainer.Resolve<HttpClient>();
            var httpClient = context.ScenarioContainer.Resolve<HttpClient>();
            var request = await httpClient.GetAsync($"api/SubscriptionAccount/{HttpUtility.UrlEncode(identifierContainer.Id)}");
            var bodyString = await request.Content.ReadAsStringAsync();
            var customerReadModel = JsonConvert.DeserializeObject<SubscriptionAccountReadModelDto>(bodyString);
            Assert.That(customerReadModel, Is.Not.Null);
            Assert.That(customerReadModel.Id, Is.EqualTo(identifierContainer.Id));
            Assert.That(customerReadModel.Subscriber, Is.EqualTo(context.Get<CreateSubscriptionAccountDto>("creationSubscriptionAccountCommand").Subscriber));
        }

        [Then(@"get request return '(.*)' subscription account description")]
        public void ThenGetRequestReturnSubscriptionAccountDescription(string customer)
        {
            var dtoModel = context.Get<SubscriptionAccountReadModelDto>();
            Assert.That(dtoModel, Is.Not.Null);
            Assert.That(dtoModel.Subscriber, Is.EqualTo(customer).Using((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase));
            Assert.That(dtoModel.SubscriptionDate.Date, Is.EqualTo(DateTime.UtcNow.Date));
        }

    }
}
