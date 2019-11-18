using Akkatecture.Aggregates;
using Akkatecture.Events;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription.Events
{
    [EventVersion("SubscriptionCreated", 1)]
    public class SubscriptionCreatedEvent : AggregateEvent<Subscription, SubscriptionId>
    {
        public string SubscriptionStatus { get; }
        public string SubscriptionUser { get; }
        public string SubscriptionType { get; }
        public DateTime SubscriptionDate { get; }


        public SubscriptionCreatedEvent(string subscriptionStatus, string subscriptionUser, string subscriptionType, DateTime subscriptionDate)
        {
            SubscriptionStatus = subscriptionStatus;
            SubscriptionUser = subscriptionUser;
            SubscriptionType = subscriptionType;
            SubscriptionDate = subscriptionDate;
        }
    }
}
