using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Events
{
    public class SubscriptionCreatedEvent : AggregateEvent<SubscriptionAccount, SubscriptionAccountId>
    {
        public SubscriptionStatus SubscriptionStatus { get; }

        public SubscriptionCreatedEvent(SubscriptionStatus subscriptionStatus)
        {
            SubscriptionStatus = subscriptionStatus;
        }
    }
}
