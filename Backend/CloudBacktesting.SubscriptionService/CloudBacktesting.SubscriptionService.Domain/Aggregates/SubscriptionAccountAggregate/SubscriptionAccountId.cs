using EventFlow.Core;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate
{
    public class SubscriptionAccountId : Identity<SubscriptionAccountId>
    {
        public SubscriptionAccountId(string value) : base(value)
        {
        }
    }
}
