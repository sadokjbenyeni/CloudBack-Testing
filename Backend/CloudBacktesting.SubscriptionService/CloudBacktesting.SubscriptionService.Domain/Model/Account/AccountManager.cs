using Akkatecture.Aggregates;
using Akkatecture.Commands;


namespace CloudBacktesting.SubscriptionService.Domain.Model.Subscription
{
    public class AccountManager : AggregateManager<Account, AccountId, Command<Account, AccountId>>
    {
    }
}
