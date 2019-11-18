using Akkatecture.Core;
using Akkatecture.ValueObjects;
using Newtonsoft.Json;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class SubscriptionId : Identity<SubscriptionId>
    {
        public SubscriptionId(string value) : base(value)
        {
        }
    }
}
