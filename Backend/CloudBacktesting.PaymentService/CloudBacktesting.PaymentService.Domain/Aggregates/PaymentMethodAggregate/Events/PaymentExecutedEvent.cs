using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Sagas;
using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events
{
    public class PaymentExecutedEvent : AggregateEvent<PaymentMethod, PaymentMethodId>, IBillingSagaItemId
    {
        public string MerchantTransactionId { get; }
        public string ItemId { get; }
        public string PaymentMethodId { get; }
        public string Subscriber { get; }
        public Card CardDetails { get; }
        public string Type { get; }
        public string Currency { get; }

        public PaymentExecutedEvent(string merchantTransactionId, string billingItemId, string paymentMethodId, string subscriber, Card cardDetails, string type, string currency)
        {
            MerchantTransactionId = merchantTransactionId;
            ItemId = billingItemId;
            PaymentMethodId = paymentMethodId;
            Subscriber = subscriber;
            CardDetails = cardDetails;
            Type = type;
            Currency = currency;
        }
    }
}
