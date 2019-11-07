using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Model.Subscription.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Subscription
{
    public class AccountState : AggregateState<Account, AccountId>
    {
        public State Creating { get; set; }
    }
}
