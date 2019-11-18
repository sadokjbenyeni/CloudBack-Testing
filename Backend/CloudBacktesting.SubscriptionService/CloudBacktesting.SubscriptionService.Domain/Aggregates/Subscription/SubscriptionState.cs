using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription.Events;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription
{
    public class SubscriptionState : AggregateState<Subscription, SubscriptionId>, IApply<SubscriptionCreatedEvent>
    {
        public string SubscriptionStatus { get; set; }
        public string SubscriptionUser { get; set; }
        public string SubscriptionType { get; set; }
        public DateTime SubscriptionDate { get; set; }

        public void Apply(SubscriptionCreatedEvent aggregateEvent)
        {
            SubscriptionStatus = aggregateEvent.SubscriptionStatus;
            SubscriptionUser = aggregateEvent.SubscriptionUser;
            SubscriptionType = aggregateEvent.SubscriptionType;
            SubscriptionDate = aggregateEvent.SubscriptionDate;
        }
    }
}
