

using CloudBacktesting.PaymentService.Infra.Models;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Infra.PaymentServices.CardServices
{
    public class Smart2PayCardService : ISmart2PayCardService
    {
        private readonly ICardPaymentService cardPaymentService;
        private double amount = 0;

        public Smart2PayCardService(ICardPaymentService cardPaymentService)
        {
            this.cardPaymentService = cardPaymentService;
        }

        public async Task<bool> CreateAsync(string merchantTransactionId, string subscriber, Card cardDetails, string type, string currency, CancellationToken cancellationToken)
        {
            TypeToAmountConverter(type);

            var apiPayment = new CardPaymentRequest
            {
                MerchantTransactionID = merchantTransactionId,
                Amount = ConvertAmount(amount),
                Currency = currency,
                Description = $"{subscriber} is paying for {amount} {currency} at {DateTime.Now}",

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
                    SecurityCode = cardDetails.SecurityCode
                },

                Capture = true

            }.ToApiCardPaymentRequest();

            var result = await cardPaymentService.CreatePaymentAsync(apiPayment, cancellationToken);
            return result.IsSuccess;
        }

        private void TypeToAmountConverter(string type)
        {
            if (type == "Dedicated")
            {
                amount = 50;
            }
            else if (type == "Mutualized")
            {
                amount = 100;
            }
            else if (type == "Premium")
            {
                amount = 200;
            }
        }

        private long ConvertAmount(double amount)
        {
            return (long)(amount * 100);
        }
    }
}
