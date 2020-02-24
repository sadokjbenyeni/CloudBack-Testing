using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class SubscriptionNPaymentToBillingLinkedEvent : AggregateEvent<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; }
        public string SubscriptionRequestId { get; }
        public string PaymentMethodId { get; }
        public string PaymentMethodStatus { get; }
        public string SubscriptionType { get; }


        public SubscriptionNPaymentToBillingLinkedEvent(string itemId, string subscriptionRequestId, string paymentMethodId, string paymentMethodStatus, string subscriptionType)
        {
            ItemId = itemId;
            SubscriptionRequestId = subscriptionRequestId;
            PaymentMethodId = paymentMethodId;
            PaymentMethodStatus = paymentMethodStatus;
            SubscriptionType = subscriptionType;
        }

    }
}
