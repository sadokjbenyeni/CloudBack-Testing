using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events
{
    public class PaymentMethodCreatedEvent : AggregateEvent<PaymentMethod, PaymentMethodId>, IPaymentSagaMethodId
    {
        public string MethodId { get; set; }
        public string PaymentAccountId { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }

        public PaymentMethodCreatedEvent(string methodId, string paymentAccountId, string cardNumber, string cardType, string cardHolder, DateTime expirationDate)
        {
            MethodId = methodId;
            PaymentAccountId = paymentAccountId;
            CardNumber = cardNumber;
            CardType = cardType;
            CardHolder = cardHolder;
            ExpirationDate = expirationDate;
        }
    }
}
