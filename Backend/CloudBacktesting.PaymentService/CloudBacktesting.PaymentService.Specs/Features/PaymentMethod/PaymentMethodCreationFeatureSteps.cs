using CloudBacktesting.Infra.Security;
using CloudBacktesting.PaymentService.Specs.Host;
using CloudBacktesting.PaymentService.WebAPI.Models;
using CloudBacktesting.PaymentService.WebAPI.Models.PaymentMethod;
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

namespace CloudBacktesting.PaymentService.Specs.Features.PaymentMethod
{
    [Binding]
    public class PaymentMethodCreationFeatureSteps
    {
        private readonly ScenarioContext context;

        public PaymentMethodCreationFeatureSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"(.*) credit cards has been already created by '(.*)'")]
        public async Task GivenCreditCardsHasBeenAlreadyCreatedByChang(int numberOfCreditCards, string user)
        {
            var creditCardsDto = Enumerable.Range(0, numberOfCreditCards)
                                           .Select(index => new CreateCardPaymentMethodDto
                                           {
                                               Holder = $"{user}'s Company, Card n°{numberOfCreditCards}",
                                               Numbers = "4713 7422 6653 7429",
                                               Network = "Visa",
                                               Cryptogram = "509",
                                               ExpirationYear = "2021",
                                               ExpirationMonth = "02"
                                           });
            var userIdentity = context.Get<UserIdentity>(user);
            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(userIdentity);
            var listCreditCard = new List<PaymentMethodDto>();
            foreach(var creditCard in creditCardsDto)
            {
                var contentString = new StringContent(JsonConvert.SerializeObject(creditCard), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync("/api/v1/PaymentMethod", contentString);
                var stringValue = await result.Content.ReadAsStringAsync();
                var identifier = JsonConvert.DeserializeObject<IdentifierDto>(stringValue);
                listCreditCard.Add(new PaymentMethodDto()
                {
                    CardHolder = creditCard.Holder,
                    CardNumber = creditCard.Numbers,
                    CardType = creditCard.Network,
                    Cryptogram = creditCard.Cryptogram,
                    ExpirationYear = creditCard.ExpirationYear,
                    ExpirationMonth = creditCard.ExpirationMonth,
                    PaymentAccountId = identifier.Id
                });
            }
            context.Add("creditCardInDataBase", listCreditCard);
        }
        
        [Given(@"'(.*)' created payment method with:")]
        [When(@"'(.*)' creates a new payment method with:")]
        public async Task WhenCreatesANewPaymentMethodWith(string user, Table table)
        {
            var command = table.CreateInstance<CreateCardPaymentMethodDto>();
            var userIdentity = context.Get<UserIdentity>(user);
            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(userIdentity);
            var contentString = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync("/api/v1/PaymentMethod", contentString);
            context.Add("PostPaymentMethodCommand", command);
            context.Add("PostPaymentMethodResult", result);
        }
        
        [When(@"'(.*)' browses all payment method")]
        public async Task WhenBrowsesAllPaymentMethod(string user)
        {
            var userIdentity = context.Get<UserIdentity>(user);
            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(userIdentity);
            var result = await httpClient.GetAsync("/api/v1/PaymentMethod");
            context.Add("BrowsesAllPaymentMethod", result);
        }

        [When(@"'(.*)' browses '(.*)' payment method")]
        public async Task WhenBrowsesSCompanyPaymentMethod(string user, string creditCardHolder)
        {
            var userIdentity = context.Get<UserIdentity>(user);
            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(userIdentity);
            var paymentMethodId = await SearchPaymentIdInContext(creditCardHolder);
            var result = await httpClient.GetAsync($"/api/v1/PaymentMethod/{HttpUtility.UrlEncode(paymentMethodId)}");
            context.Add("BrowsesSpecificPaymentMethod", result);
            context.Add("BrowsesSpecificPaymentIdentifier", paymentMethodId);
        }

        [When(@"'(.*)' browses wrong id payment method")]
        public async Task WhenBrowsesWrongIdPaymentMethod(string user)
        {
            var userIdentity = context.Get<UserIdentity>(user);
            var httpClient = context.ScenarioContainer.Resolve<ITestHttpClientFactory>().Create(userIdentity);
            var result = await httpClient.GetAsync($"/api/v1/PaymentMethod/{HttpUtility.UrlEncode($"paymentmethod-{Guid.NewGuid()}")}");
            context.Add("BrowsesWrongPaymentMethod", result);
        }
        
        [Then(@"Creation of payment method creation is successful")]
        public async Task ThenCreationOfPaymentMethodIsSuccessful()
        {
            if (!context.TryGetValue<HttpResponseMessage>("PostPaymentMethodResult", out var response))
            {
                Assert.Fail("Result of the creation payment method request is not found");
            }
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(await response.Content.ReadAsStringAsync(), Is.Not.Null.And.Not.Empty);
        }
        
        [Then(@"Creation of payment method creation is not successful")]
        public void ThenCreationOfPaymentMethodIsNotSuccessful()
        {
            if (!context.TryGetValue<HttpResponseMessage>("PostPaymentMethodResult", out var response))
            {
                Assert.Fail("Result of the creation payment method request is not found");
            }
            Assert.That(response.IsSuccessStatusCode, Is.False);
        }
        
        [Then(@"result of request is empty")]
        public async Task ThenResultOfRequestIsEmpty()
        {
            if(!context.TryGetValue<HttpResponseMessage>("BrowsesAllPaymentMethod", out var httpResponse))
            {
                Assert.Fail("Result of the creation payment method request is not found");
            }
            var list = JsonConvert.DeserializeObject<IEnumerable<PaymentMethodDto>>(await httpResponse.Content.ReadAsStringAsync());
            Assert.That(list, Is.Empty.Or.Null);
        }
        
        [Then(@"the credit card has been return by the request")]
        public async Task ThenTheCreditCardHasBeenReturnByTheRequest()
        {
            if(!context.TryGetValue<CreateCardPaymentMethodDto>("PostPaymentMethodCommand", out var command))
            {
                Assert.Fail("Cannot found information in BDD to find the good payment method");
            }
            if (!context.TryGetValue<HttpResponseMessage>("BrowsesAllPaymentMethod", out var httpResponse))
            {
                Assert.Fail("Result of the creation payment method request is not found");
            }
            var list = JsonConvert.DeserializeObject<IEnumerable<PaymentMethodDto>>(await httpResponse.Content.ReadAsStringAsync());
            Assert.That(list.SingleOrDefault(item => string.Equals(item.CardNumber, command.Numbers)), Is.Not.Null);
        }
        
        [Then(@"the result of the request contains (.*) credit cards created")]
        public async Task ThenTheResultOfTheRequestContainsCreditCardsCreated(int numberOfCreditCards)
        {
            if (!context.TryGetValue<HttpResponseMessage>("BrowsesAllPaymentMethod", out var httpResponse))
            {
                Assert.Fail("Result of the creation payment method request is not found");
            }
            var list = JsonConvert.DeserializeObject<IEnumerable<PaymentMethodDto>>(await httpResponse.Content.ReadAsStringAsync());
            Assert.That(list.ToList(), Has.Count.EqualTo(numberOfCreditCards));
        }
        
        [Then(@"only this credit cards has been returned")]
        public async Task ThenOnlyThisCreditCardsHasBeenReturned()
        {
            if(!context.TryGetValue<string>("BrowsesSpecificPaymentIdentifier", out var identifier))
            {
                Assert.Fail("Cannot found the identifier of the request");
            }
            if(!context.TryGetValue<HttpResponseMessage>("BrowsesSpecificPaymentMethod", out var response))
            {
                Assert.Fail("Cannot found the http response message");
            }
            Assert.That(response.IsSuccessStatusCode, Is.True);
            var paymentMethodStr = await response.Content.ReadAsStringAsync();
            var paymentMethod = JsonConvert.DeserializeObject<PaymentMethodReadModelDto>(paymentMethodStr);
            Assert.That(paymentMethod, Is.Not.Null);
            Assert.That(paymentMethod.PaymentMethodId, Is.EqualTo(identifier));
        }
        
        [Then(@"the api return an not found result")]
        public void ThenTheApiReturnAnNotFoundResult()
        {
            if (!context.TryGetValue<HttpResponseMessage>("BrowsesWrongPaymentMethod", out var response))
            {
                Assert.Fail("Cannot found the http response message");
            }
            Assert.That(response.IsSuccessStatusCode, Is.False);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }


        private async Task<string> SearchPaymentIdInContext(string creditCardHolder)
        {
            if (context.TryGetValue<CreateCardPaymentMethodDto>("PostPaymentMethodCommand", out var command)
                && string.Equals(command.Holder, creditCardHolder)
                && context.TryGetValue<HttpResponseMessage>("PostPaymentMethodResult", out var response)
                && response.IsSuccessStatusCode
                && response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IdentifierDto>(await response.Content.ReadAsStringAsync()).Id;
            }
            if (context.TryGetValue<List<PaymentMethodDto>>("creditCardInDataBase", out var listCb))
            {
                return listCb.FirstOrDefault(cb => string.Equals(cb.CardHolder, creditCardHolder))?.PaymentAccountId;
            }
            return null;
        }

    }
}
