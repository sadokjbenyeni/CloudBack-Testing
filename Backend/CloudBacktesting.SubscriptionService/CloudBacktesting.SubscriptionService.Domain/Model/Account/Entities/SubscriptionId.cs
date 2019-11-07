using Akkatecture.Core;
using Akkatecture.ValueObjects;
using Newtonsoft.Json;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Account.Entities
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class SubscriptionId : Identity<SubscriptionId>
    {
        public SubscriptionId(string value) : base(value)
        {

        }
    }
}
