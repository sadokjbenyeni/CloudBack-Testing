using Akkatecture.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Subscription
{
    public class Account : AggregateRoot<Account, AccountId, AccountState>
    {
        public Account(AccountId aggregateId): base(aggregateId)
        {

        }
    }
}
