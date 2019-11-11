using Akkatecture.Aggregates;
using Akkatecture.Commands;


namespace CloudBacktesting.SubscriptionService.Domain.Model
{
    public class SubscriptionAccountManager : AggregateManager<SubscriptionAccount, SubscriptionAccountId, Command<SubscriptionAccount, SubscriptionAccountId>>
    {
    }
}
