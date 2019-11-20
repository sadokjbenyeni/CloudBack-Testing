using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription.Commands;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription
{
    /// <Summary>
    /// This command create a new subscription for one user
    /// <Summary>
    public class Subscription /*: AggregateRoot<Subscription, SubscriptionId, SubscriptionState>*/
    {
        public Subscription(SubscriptionId id)/*: base(id)*/
        {
        }
    }
}
