using CloudBacktesting.PaymentService.Domain.Sagas;
using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events
{
    public class BillingItemLinkedEvent : AggregateEvent<PaymentMethod, PaymentMethodId>, IBillingSagaItemId
    {
        public string ItemId { get; }
        public string MerchantTransactionId { get; }
        public string PaymentMethodStatus { get; }
        public string PaymentMethodSubscriber { get; }
        public Card PaymentMethodCardDetails { get; }
        public string Type { get; }
        public string SubscriptionRequestId { get; internal set; }
        public Card CardDetails { get; internal set; }
        public string Subscriber { get; internal set; }

        public BillingItemLinkedEvent(string itemId, string merchantTransactiontId, string status, string subscriber, Card card, string type)
        {
            ItemId = itemId;
            MerchantTransactionId = merchantTransactiontId;
            PaymentMethodStatus = status;
            PaymentMethodSubscriber = subscriber;
            PaymentMethodCardDetails = card;
            Type = type;
        }
    }
}
