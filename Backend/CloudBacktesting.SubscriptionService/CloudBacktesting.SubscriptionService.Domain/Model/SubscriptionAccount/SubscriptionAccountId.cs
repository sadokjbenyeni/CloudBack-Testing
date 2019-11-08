using Akkatecture.Core;
using Akkatecture.ValueObjects;
using Newtonsoft.Json;

namespace CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class SubscriptionAccountId : Identity<SubscriptionAccountId>
    {
        public SubscriptionAccountId(string value) : base(value)
        {

        }
    }
}
