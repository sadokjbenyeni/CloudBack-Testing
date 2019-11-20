using EventFlow.Aggregates;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount.Events
{

    public class SubscriptionAccountCreatedEvent : AggregateEvent<SubscriptionAccount, SubscriptionAccountId>
    {
        public string Subscriber { get; }
        public DateTime SubscriptionDate { get; }

        public SubscriptionAccountCreatedEvent(string subscriber, DateTime subscriptionDate)
        {
            Subscriber = subscriber;
            SubscriptionDate = subscriptionDate;
        }
    }
}
