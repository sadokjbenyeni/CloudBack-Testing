

using CloudBacktesting.PaymentService.Infra.Models;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Services;
using System;
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

        public async Task<bool> CreateAsync(string merchantTransactionId, string subscriber, Card cardDetails, BillingAddress billingaddress, double amount, string currency, CancellationToken cancellationToken)
        {
            var apiPayment = new CardPaymentRequest
            {
                MerchantTransactionID = merchantTransactionId,
                Amount = ConvertAmount(amount),
                Currency = currency,
                Description = $"{subscriber} is paying {amount} {currency} at {DateTime.Now}",

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

                BillingAddress = new Address
                {
                    City = billingaddress.City,
                    ZipCode = billingaddress.ZipCode,
                    State = billingaddress.State,
                    Street = billingaddress.Street,
                    Country = billingaddress.Country
                },

                Capture = true

            }.ToApiCardPaymentRequest();

            var result = await cardPaymentService.CreatePaymentAsync(apiPayment, cancellationToken);
            return result.IsSuccess;
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
