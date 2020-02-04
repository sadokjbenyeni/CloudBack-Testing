using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Authentication;
using S2p.RestClient.Sdk.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Infra.PaymentServices.CardServices
{
    public class Smart2PayCardService
    {
        private readonly ICardPaymentService cardPaymentService;

        public Smart2PayCardService(ICardPaymentService cardPaymentService)
        {
            this.cardPaymentService = cardPaymentService;
        }

        public async Task<CardPaymentResponse> CreateAsync(string paymentClientId, string subscriber, CardInformation cardDetails, double amount, string currency, CancellationToken cancellationToken)
        {
            var apiPayment = new CardPaymentRequest
            {
                MerchantTransactionID = paymentClientId,
                Amount = ConvertAmount(amount),
                Currency = currency,
                Description = $"{subscriber} payes {amount} {currency} at {DateTime.Now}",

                Customer = new Customer()
                {
                    Email = subscriber,
                },

                Card = new CardDetailsRequest
                {
                    ExpirationMonth = cardDetails.ExpirationMonth,
                    ExpirationYear = cardDetails.ExpirationYear,
                    HolderName = cardDetails.HolderName,
                    Number = cardDetails.Number,
                    RequireSecurityCode = true,
                    SecurityCode = cardDetails.SecurityCode
                },

                Capture = false,
                Retry = false,
                GenerateCreditCardToken = false,
                PaymentTokenLifetime = 5
            }.ToApiCardPaymentRequest();

            var result = await cardPaymentService.CreatePaymentAsync(apiPayment, cancellationToken);
            var response = result.Value.Payment;
            return response;
        }

        private long ConvertAmount(double amount)
        {
            return (long)(amount * 100);
        }

        //public async Task Create(CardDetails card)
        //{
        //    var reqgdfg
        //    await cardPaymentService.CreatePaymentAsync();
        //}
    }
}
