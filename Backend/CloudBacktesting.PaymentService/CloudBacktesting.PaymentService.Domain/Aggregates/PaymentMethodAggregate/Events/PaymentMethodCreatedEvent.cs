using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events
{
    public class PaymentMethodCreatedEvent : AggregateEvent<PaymentMethod, PaymentMethodId>, IPaymentSagaMethodId
    {
        public string MethodId { get; }
        public string PaymentAccountId { get; }
        public string CardNumber { get; }
        public string CardType { get; }
        public string CardHolder { get; }
        public string Cryptogram { get; }
        public string ExpirationYear { get; }
        public string ExpirationMonth { get; }
        public string Status { get; }

        public PaymentMethodCreatedEvent(string methodId, string paymentAccountId, string status, string cardNumber, string cardType, string cardHolder, string cryptogram, string expirationYear, string expirationMonth)
        {
            MethodId = methodId;
            PaymentAccountId = paymentAccountId;
            Status = status;
            CardNumber = cardNumber;
            CardType = cardType;
            CardHolder = cardHolder;
            Cryptogram = cryptogram;
            ExpirationYear = expirationYear;
            ExpirationMonth = expirationMonth;
        }
    }
}
