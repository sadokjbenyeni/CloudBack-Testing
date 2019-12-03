using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Models.Account.Client.SubscriptionAccount;
using CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Client.SubscriptionRequest;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CloudBacktesting.SubscriptionService.Specs.Features.SubscriptionRequest
{
    [Binding]
    public class SubscriptionRequestFeatureSteps
    {
        private readonly ScenarioContext context;

        public SubscriptionRequestFeatureSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"'(.*)' subscription has been created for '(.*)'")]
        public async Task GivenSubscriptionHasBeenCreatedFor(string typeOfSubscription, string customer)
        {
            await CreateSubscriptionRequest(typeOfSubscription);
            var result = context.Get<HttpResponseMessage>("subscriptionRequestCommandResult");
            var identifier = JsonConvert.DeserializeObject<IdentifierDto>(await result.Content.ReadAsStringAsync());
            SetInContextInList(customer, identifier);
        }

        private void SetInContextInList(string customer, IdentifierDto identifier)
        {
            if(!context.ContainsKey($"{customer}-subscriptionRequest"))
            {
                context.Set(new List<string>(), $"{customer}-subscriptionRequest");
            }
            context.Get<List<string>>($"{customer}-subscriptionRequest").Add(identifier.Id);
        }

        [When(@"Chang sends new request of subscription for (.*) service")]
        public async Task WhenChangSendsNewRequestOfSubscriptionForMutualizedService(string typeOfSubscription)
        {
            await CreateSubscriptionRequest(typeOfSubscription);
        }

        private async Task CreateSubscriptionRequest(string typeOfSubscription)
        {
            var resultSubscriptionAccountCreate = context.Get<HttpResponseMessage>("createSubscriptionCommandResult");
            var subscriptionAccountIdentifier = JsonConvert.DeserializeObject<SubscriptionAccountIdDto>((await resultSubscriptionAccountCreate.Content.ReadAsStringAsync()));
            var httpClient = context.ScenarioContainer.Resolve<HttpClient>();
            var dtoModel = new CreateSubscriptionRequestDto()
            {
                SubscriptionAccountId = subscriptionAccountIdentifier.Id,
                Type = typeOfSubscription,
            };
            context.Set(dtoModel, "subscriptionRequestCommand");
            var commandResult = await httpClient.PostAsync("api/subscriptionrequest", new StringContent(JsonConvert.SerializeObject(dtoModel), Encoding.UTF8, "application/json"));
            context.Set(commandResult, "subscriptionRequestCommandResult");
        }

        [When(@"'(.*)' sends GET request these subscriptions")]
        public async Task WhenSendsGETRequestTheseSubscriptions(string customer)
        {
            var httpClient = context.ScenarioContainer.Resolve<HttpClient>();
            var result = await httpClient.GetAsync("api/subscriptionrequest");
            context.Set(result, "getSubscriptionRequestAll");
        }


        [When(@"'(.*)' sends GET request with '(.*)' subscription")]
        public void WhenSendsGETRequestWithSubscription(string p0, string p1)
        {
            context.Pending();
        }
        
        [Then(@"Creation of subscription is successful")]
        public async Task ThenCreationOfSubscriptionIsSuccessful()
        {
            var result = context.Get<HttpResponseMessage>("subscriptionRequestCommandResult");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var identifier = JsonConvert.DeserializeObject<IdentifierDto>(await result.Content.ReadAsStringAsync());
            Assert.That(identifier?.Id, Is.Not.Null.And.Not.Empty);
        }
        
        [Then(@"all subscription has been return at '(.*)'")]
        public void ThenAllSubscriptionHasBeenReturnAt(string p0)
        {
            
        }

        [Then(@"subscriptions are returned for '(.*)'")]
        public async Task ThenSubscriptionsAreReturnedFor(string customer)
        {
            var subscriptionRequestIds = context.Get<List<string>>($"{customer}-subscriptionRequest");
            var httpMessage = context.Get<HttpResponseMessage>("getSubscriptionRequestAll");
            Assert.That(httpMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var actual = JsonConvert.DeserializeObject<IEnumerable<SubscriptionRequestReadModelDto>>(await httpMessage.Content.ReadAsStringAsync()).ToList();
            Assert.That(actual, Has.Count.EqualTo(subscriptionRequestIds.Count));
            Assert.That(actual.Select(dto => dto.Id), Is.EquivalentTo(subscriptionRequestIds));
        }


        [Then(@"only '(.*)' subscription has been return at '(.*)'")]
        public void ThenOnlySubscriptionHasBeenReturnAt(string p0, string p1)
        {
            context.Pending();
        }

        [Then(@"The subscription required that:")]
        public async Task ThenTheSubscriptionRequiredThat(Table table)
        {
            var httpMessage = context.Get<HttpResponseMessage>("getSubscriptionRequestById");
            Assert.That(httpMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var actual = JsonConvert.DeserializeObject<SubscriptionRequestReadModelDto>(await httpMessage.Content.ReadAsStringAsync());
            var expected = table.CreateInstance<SubscriptionRequestReadModelDto>();

            Assert.That(actual.Status, Is.EqualTo(expected.Status));
            Assert.That(actual.Type, Is.EqualTo(expected.Type));
            Assert.That(actual.Subscriber, Is.EqualTo(expected.Subscriber));
            Assert.That(actual.IsSystemValidated, Is.EqualTo(expected.IsSystemValidated));
        }

        [When(@"'(.*)' sends GET request subscription")]
        public async Task WhenSendsGETRequestSubscription(string customer)
        {
            var id = context.Get<List<string>>($"{customer}-subscriptionRequest")?.First();
            var httpClient = context.ScenarioContainer.Resolve<HttpClient>();
            var result = await httpClient.GetAsync($"api/subscriptionrequest/{id}");
            context.Set(result, $"getSubscriptionRequestById");
        }


    }
}
