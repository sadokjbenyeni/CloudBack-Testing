

using EventFlow.Core;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount
{
    public class SubscriptionAccountId : Identity<SubscriptionAccountId>
    {
        public SubscriptionAccountId(string value) : base(value)
        {
        }
    }
}
