using CloudBacktesting.PaymentService.Infra.PaymentServices.CardServices;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
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
        public async Task SHould_payment_by_credit_card_is_valid_when_usual_case()
        {
            var s2pCardService = Substitute.For<ICardPaymentService>();
            s2pCardService.CreatePaymentAsync(Arg.Any<ApiCardPaymentRequest>(), Arg.Any<CancellationToken>())
                .Returns(info => ReturnSuccessRequest(info));
            var service = new Smart2PayCardService(s2pCardService);
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
            var httpRequest = Substitute.ForPartsOf<HttpRequestMessage>(HttpMethod.Post, "spec.api.smart2pay.com/CreatePayment");
            var httpResponse = Substitute.ForPartsOf<HttpResponseMessage>(HttpStatusCode.OK);
            return ApiResult.Success<ApiCardPaymentResponse>(httpRequest, httpResponse, string.Empty, response);
        }
    }
}
