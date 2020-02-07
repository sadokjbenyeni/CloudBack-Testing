using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class SubscriptionRequestToBillingItemLinkedEvent : AggregateEvent<BillingItem, BillingItemId>
    {
        public string SubscriptionRequestId { get; set; }

        public SubscriptionRequestToBillingItemLinkedEvent(string subscriptionRequestId)
        {
            SubscriptionRequestId = subscriptionRequestId;
        }

    }
}
