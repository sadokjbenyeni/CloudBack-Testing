using EventFlow.Aggregates;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Events
{

    public class SubscriptionAccountCreatedEvent : AggregateEvent<SubscriptionAccount, SubscriptionAccountId>
    {
        public string Subscriber { get; }

        public SubscriptionAccountCreatedEvent(string subscriber)
        {
            Subscriber = subscriber;
        }
    }
}
