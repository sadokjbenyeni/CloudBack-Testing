using Akkatecture.Aggregates;
using Akkatecture.Commands;


namespace CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount
{
    public class SubscriptionAccountManager : AggregateManager<SubscriptionAccount, SubscriptionAccountId, Command<SubscriptionAccount, SubscriptionAccountId>>
    {
    }
}
