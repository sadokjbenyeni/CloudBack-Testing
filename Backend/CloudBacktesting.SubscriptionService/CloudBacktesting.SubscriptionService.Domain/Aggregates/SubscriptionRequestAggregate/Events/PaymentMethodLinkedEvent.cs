using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class PaymentMethodLinkedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>
    {
        public string PaymentMethodId { get; set; }
        public string PaymentAccountId { get; set; }

        public PaymentMethodLinkedEvent(string paymentMethodId, string paymentAccoutId)
        {
            PaymentMethodId = paymentMethodId;
            PaymentAccountId = paymentAccoutId;
        }
    }
}
