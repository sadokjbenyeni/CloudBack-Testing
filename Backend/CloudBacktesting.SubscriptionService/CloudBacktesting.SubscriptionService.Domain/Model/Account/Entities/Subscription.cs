
using Akkatecture.Entities;
using CloudBacktesting.SubscriptionService.Domain.Model.Subscription;
using CloudBacktesting.SubscriptionService.Domain.Model.Subscription.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Account.Entities
{
    public class Subscription : Entity<SubscriptionId>
    {
        public AccountId User { get; }
        public State SubscriptionState { get; }

        public Subscription( SubscriptionId id, AccountId user, State subscriptionState) : base(id)
        {
            User = User;
            SubscriptionState = subscriptionState;
        }
    }
}
