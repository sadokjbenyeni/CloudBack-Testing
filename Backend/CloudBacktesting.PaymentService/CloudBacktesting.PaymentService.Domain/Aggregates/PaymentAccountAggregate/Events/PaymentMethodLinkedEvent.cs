using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events
{
    public class PaymentMethodLinkedEvent : AggregateEvent<PaymentAccount, PaymentAccountId>, IPaymentSagaMethodId
    {
        public string MethodId { get; }
        public string Client { get; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string Cryptogram { get; set; }

        public PaymentMethodLinkedEvent(string methodId, string client, string cardNumber, string cardType, string  cryptogram)
        {
            MethodId = methodId;
            Client = client;
            CardNumber = cardNumber;
            CardType = cardType;
            Cryptogram = cryptogram;
        }
    }
}
