﻿using CloudBacktesting.Infra.Security;
using CloudBacktesting.PaymentService.Infra.PaymentServices.CardServices;
using CloudBacktesting.PaymentService.Infra.Tests.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Authentication;
using S2p.RestClient.Sdk.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Infra.Tests.PaymentServices.CardServices
{
    [TestFixture]
    public class Smart2PayCardPaymentServicesTests
    {
        [Test]
        public async Task Should_payment_by_credit_card_is_valid_when_usual_case()
        {
            var baseAddress = new Uri("https://securetest.smart2pay.com");

            IHttpClientBuilder httpClientBuilder = new HttpClientBuilder(() => new AuthenticationConfiguration
            {
                SiteId = 33258,
                ApiKey = "JOPxYQftN9xICry9koMuER6L4SrszVHI8SLh9Q83n964tFa2GK"
            });

            var httpClient = httpClientBuilder.Build();
            var paymentService = new CardPaymentService(httpClient, baseAddress);

            var s2pCardService = Substitute.For<ICardPaymentService>();
            s2pCardService.CreatePaymentAsync(Arg.Any<ApiCardPaymentRequest>(), Arg.Any<CancellationToken>())
                .Returns(info => ReturnSuccessRequest(info));
            var service = new Smart2PayCardService(s2pCardService);

            var paymentRequest = new CardPaymentRequest
            {
                MerchantTransactionID = "MerchantTransactionID",
                Amount = 9000,
                Currency = "USD",
                ReturnURL = "http://demo.smart2pay.com/redirect.php",
                Description = "DescriptionText",
                StatementDescriptor = "bank statement message",
                Card = new CardDetailsRequest
                {
                    HolderName = "John Doe",
                    Number = "4111111111111111",
                    ExpirationMonth = "02",
                    ExpirationYear = "2022",
                    RequireSecurityCode = false
                },
                BillingAddress = new Address
                {
                    City = "Iasi",
                    ZipCode = "7000-49",
                    State = "Iasi",
                    Street = "Sf Lazar",
                    StreetNumber = "37",
                    HouseNumber = "5A",
                    HouseExtension = "-",
                    Country = "BR"
                },
                Capture = false,
                Retry = false,
                GenerateCreditCardToken = false,
                PaymentTokenLifetime = 5
            }.ToApiCardPaymentRequest();

            var request = await paymentService.CreatePaymentAsync(paymentRequest);
            bool response = await service.CreateAsync("IdPaymenet", "chang@trade.com", new CardInformation(), 2000, "EUR", CancellationToken.None);
            Assert.That(response, Is.True);

        }

        [Test]
        public async Task Should_around_amount_when_amount_has_decimal_values()
        {
            var s2pCardService = Substitute.For<ICardPaymentService>();

            s2pCardService.CreatePaymentAsync(Arg.Any<ApiCardPaymentRequest>(), Arg.Any<CancellationToken>())
                .Returns(info => ReturnSuccessRequest(info));
            s2pCardService.When(sp => sp.CreatePaymentAsync(Arg.Any<ApiCardPaymentRequest>(), Arg.Any<CancellationToken>()))
                          .Do(info => Assert.That(info.Arg<ApiCardPaymentRequest>().Payment.Amount, Is.EqualTo((long)200015)));

            var service = new Smart2PayCardService(s2pCardService);
            bool response = await service.CreateAsync("IdPaymenet", "chang@trade.com", new CardInformation(), 2000.15452, "EUR", CancellationToken.None);
            Assert.That(response, Is.True);

        }

        private ApiResult<ApiCardPaymentResponse> ReturnSuccessRequest(CallInfo info)
        {
            var request = info.Arg<ApiCardPaymentRequest>();
            var response = new ApiCardPaymentResponse();
            response.Payment = new CardPaymentResponse()
            {
                Amount = request.Payment.Amount,
                Card = new CardDetailsRequest()
                {

                },
                Customer = request.Payment.Customer,

            };
            var httpRequest = Substitute.ForPartsOf<HttpRequest>();
            var httpRequestMessage = Substitute.ForPartsOf<HttpRequestMessage>(HttpMethod.Post, "spec.api.smart2pay.com/CreatePayment");
            var httpResponseMessage = Substitute.ForPartsOf<HttpResponseMessage>(HttpStatusCode.OK);

            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes("67034:I1oxzWpUg857ybKk+UkIMK4gOJhlxyDBBH+n/oBE/wPIKaLnnU"));
            var header = new HeaderDictionaryTests(new Dictionary<string, StringValues>() { { "Authorization", $"Basic {token}" }, });

            httpRequest.Headers.Returns(header);

            return ApiResult.Success<ApiCardPaymentResponse>(httpRequestMessage, httpResponseMessage, string.Empty, response);
        }
    }
}
