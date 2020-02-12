using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class SubscriptionRequestToBillingItemLinkedEvent : AggregateEvent<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; set; }
        public string SubscriptionRequestId { get; set; }

        public SubscriptionRequestToBillingItemLinkedEvent(string billingItemId, string subscriptionRequestId)
        {
            ItemId = billingItemId;
            SubscriptionRequestId = subscriptionRequestId;
        }

    }
}
