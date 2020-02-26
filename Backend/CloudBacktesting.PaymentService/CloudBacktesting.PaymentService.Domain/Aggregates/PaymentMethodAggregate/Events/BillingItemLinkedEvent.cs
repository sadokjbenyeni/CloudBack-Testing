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
        public string PaymentMethodStatus { get; }
        public string PaymentMethodSubscriber { get; }
        public Card PaymentMethodCardDetails { get; }
        public string SubscriptionType { get; }
        public string SubscriptionRequestId { get; }

        public BillingItemLinkedEvent(string itemId, string status, string subscriber, Card card, string type)
        {
            ItemId = itemId;
            PaymentMethodStatus = status;
            PaymentMethodSubscriber = subscriber;
            PaymentMethodCardDetails = card;
            SubscriptionType = type;
        }
    }
}
