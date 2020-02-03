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

        public async Task<bool> CreateAsync(string paymentClientId, string subscriber, CardInformation cardDetails, double amount, string currency, CancellationToken cancellationToken)
        {
            var apiCard = new ApiCardPaymentRequest();
            var apiPayment = new CardPaymentRequest
            {
                MerchantTransactionID = "billing-" + new Guid() + DateTime.UtcNow.Year + DateTime.UtcNow.Month + DateTime.UtcNow.Day,
                Amount = ConvertAmount(amount),
                Currency = currency,

                Customer = new Customer()
                {
                    Email = subscriber,
                },

                Description = $"{subscriber} payes {amount} {currency} at {DateTime.Now}",

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
            };

            var response = await cardPaymentService.CreatePaymentAsync(apiCard, cancellationToken);
            return response.IsSuccess;
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
