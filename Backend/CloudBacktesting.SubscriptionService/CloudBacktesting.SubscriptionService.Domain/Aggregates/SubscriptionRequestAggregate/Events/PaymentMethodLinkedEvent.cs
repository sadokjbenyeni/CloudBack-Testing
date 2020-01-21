using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class PaymentMethodLinkedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>
    {
        public PaymentAction SubscriptionRequestPaymentAction { get; set; }

        public PaymentMethodLinkedEvent(PaymentAction subscriptionRequestPaymentAction)
        {
            SubscriptionRequestPaymentAction = subscriptionRequestPaymentAction;
        }
    }
}
