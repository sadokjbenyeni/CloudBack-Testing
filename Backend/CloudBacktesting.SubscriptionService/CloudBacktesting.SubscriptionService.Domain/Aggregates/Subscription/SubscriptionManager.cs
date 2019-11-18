using Akkatecture.Aggregates;
using Akkatecture.Commands;


namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription
{
    class SubscriptionManager : AggregateManager<Subscription, SubscriptionId, Command<Subscription, SubscriptionId>>
    {
    }
}
