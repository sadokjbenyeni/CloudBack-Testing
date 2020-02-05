using CloudBacktesting.Infra.Security;
using CloudBacktesting.SubscriptionService.Specs.Host;
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
using System.Web;
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
            await CreateSubscriptionRequest(customer, typeOfSubscription);
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

        private IEnumerable<string> GetByCustomer(string customer)
        {
            return context.Get<List<string>>($"{customer}-subscriptionRequest");
        }

        [When(@"Chang sends new request of subscription for (.*) service")]
        public async Task WhenChangSendsNewRequestOfSubscriptionForMutualizedService(string typeOfSubscription)
        {
            await CreateSubscriptionRequest("Chang", typeOfSubscription);
        }

        private async Task CreateSubscriptionRequest(string customer, string typeOfSubscription)
        {
            var resultSubscriptionAccountCreate = context.Get<HttpResponseMessage>("createSubscriptionCommandResult");
            var subscriptionAccountIdentifier = JsonConvert.DeserializeObject<SubscriptionAccountIdDto>((await resultSubscriptionAccountCreate.Content.ReadAsStringAsync()));

            var identity = context.Get<UserIdentity>(customer);

            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(identity);

            var dtoModel = new CreateSubscriptionRequestDto()
            {
                SubscriptionAccountId = subscriptionAccountIdentifier.Id,
                Type = typeOfSubscription,
                PaymentMethodId = "paymentmethod-6a04cf67-5576-4f9b-91b0-4b0e2c603f72",
                PaymentAccountId = "Paymentaccount-6a04cf67-5576-4f9b-91b0-4b0e2c603f72"
            };
            context.Set(dtoModel, "subscriptionRequestCommand");
            var commandResult = await httpClient.PostAsync("api/v1/subscriptionrequest", new StringContent(JsonConvert.SerializeObject(dtoModel), Encoding.UTF8, "application/json"));
            context.Set(commandResult, "subscriptionRequestCommandResult");
        }

        [When(@"'(.*)' sends GET request these subscriptions")]
        public async Task WhenSendsGETRequestTheseSubscriptions(string customer)
        {
            var identity = context.Get<UserIdentity>(customer);
            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(identity);

            var result = await httpClient.GetAsync("api/v1/subscriptionrequest");
            context.Set(result, "getSubscriptionRequestAll");
        }

        [When(@"'(.*)' sends GET request for '(.*)' subscription")]
        public async Task WhenSendsGETRequestForSubscription(string customer, string typeRequest)
        {
            var identifier = GetByCustomer(customer).Last();
            var identity = context.Get<UserIdentity>(customer);
            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(identity);
            var result = await httpClient.GetAsync($"api/v1/subscriptionrequest/{HttpUtility.UrlEncode(identifier)}");
            context.Set(result, "getSubscriptionRequest");
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
        public async Task ThenOnlySubscriptionHasBeenReturnAt(string typeOfSubscriptionRequest, string customer)
        {
            var httpResponse = context.Get<HttpResponseMessage>("getSubscriptionRequest");
            var model = JsonConvert.DeserializeObject<SubscriptionRequestReadModelDto>(await httpResponse.Content.ReadAsStringAsync());
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Subscriber, Is.EqualTo(customer).Using((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase));
            Assert.That(model.Type, Is.EqualTo(typeOfSubscriptionRequest).Using((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase));
        }

        [Then(@"The subscription required that:")]
        public async Task ThenTheSubscriptionRequiredThat(Table table)
        {
            var httpMessage = context.Get<HttpResponseMessage>("getSubscriptionRequestById");
            Assert.That(httpMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var messageContent = await httpMessage.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<SubscriptionRequestReadModelDto>>(messageContent).Last();
            var expected = table.CreateInstance<SubscriptionRequestReadModelDto>();

            Assert.That(actual.Status, Is.EqualTo(expected.Status));
            Assert.That(actual.Type, Is.EqualTo(expected.Type));
            Assert.That(actual.Subscriber, Is.EqualTo(expected.Subscriber));
            Assert.That(actual.IsSystemValidated, Is.EqualTo(expected.IsSystemValidated));
            Assert.That(actual.OrderId, Is.EqualTo(expected.OrderId));
        }

        [When(@"'(.*)' sends GET request subscription")]
        public async Task WhenSendsGETRequestSubscription(string customer)
        {
            var identity = context.Get<UserIdentity>(customer);
            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(identity);
            var result = await httpClient.GetAsync($"api/v1/subscriptionrequest");
            context.Set(result, $"getSubscriptionRequestById");
        }
    }
}
