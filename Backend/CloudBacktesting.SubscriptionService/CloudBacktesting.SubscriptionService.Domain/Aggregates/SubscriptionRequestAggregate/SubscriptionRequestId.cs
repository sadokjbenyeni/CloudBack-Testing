using EventFlow.Core;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate
{
    public class SubscriptionRequestId : Identity<SubscriptionRequestId>
    {
        public SubscriptionRequestId(string value) : base(value) { }
    }
}
