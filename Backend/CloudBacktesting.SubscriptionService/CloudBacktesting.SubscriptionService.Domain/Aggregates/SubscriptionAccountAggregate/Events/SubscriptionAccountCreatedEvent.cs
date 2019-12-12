using EventFlow.Aggregates;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Events
{

    public class SubscriptionAccountCreatedEvent : AggregateEvent<SubscriptionAccount, SubscriptionAccountId>
    {
        public string Subscriber { get; }
        public int OrderId { get; }

        public SubscriptionAccountCreatedEvent(string subscriber, int orderId)
        {
            OrderId = orderId;
            Subscriber = subscriber;
        }
    }
}
