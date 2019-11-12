using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Events
{
    public class SubscriptionCreatedEvent : AggregateEvent<SubscriptionAccount, SubscriptionAccountId>
    {
        public SubscriptionStatus SubscriptionStatus { get; }
        public SubscriptionUser SubscriptionUser { get; }
        public SubscriptionType SubscriptionType { get; }
        public SubscriptionDate SubscriptionDate { get; }


        public SubscriptionCreatedEvent(SubscriptionStatus subscriptionStatus, SubscriptionUser subscriptionUser, SubscriptionType subscriptionType, SubscriptionDate subscriptionDate)
        {
            SubscriptionStatus = subscriptionStatus;
            SubscriptionUser = subscriptionUser;
            SubscriptionType = subscriptionType;
            SubscriptionDate = subscriptionDate;
        }
    }
}
