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
            IdempotenceConfigurator();

            var apiCard = new ApiCardPaymentRequest();
            var apiPayment = new CardPaymentRequest();
            apiCard.Payment = apiPayment;
            apiPayment.Amount = ConvertAmount(amount);
            apiPayment.Currency = currency;
            apiPayment.Customer = new Customer()
            {
                Email = subscriber,
            };
            apiPayment.Description = $"{subscriber} payes {apiPayment.Amount} {currency} at {DateTime.Now}";
            apiPayment.Card = new CardDetailsRequest()
            {
                ExpirationMonth = cardDetails.ExpirationMonth,
                ExpirationYear = cardDetails.ExpirationYear,
                HolderName = cardDetails.HolderName,
                Number = cardDetails.Number,
                RequireSecurityCode = true,
                SecurityCode = cardDetails.SecurityCode,
            };
            var response = await cardPaymentService.CreatePaymentAsync(apiCard, cancellationToken);

            return response.IsSuccess;
        }

        private static void IdempotenceConfigurator()
        {
            var uniqueKeyGenerator = new Func<string>(() => { return "billing-" + new Guid() + DateTime.UtcNow.Year + DateTime.UtcNow.Month + DateTime.UtcNow.Day; });
            var httpClientBuilder = new HttpClientBuilder(() => new AuthenticationConfiguration()).WithIdempotencyKeyGenerator(uniqueKeyGenerator);
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
