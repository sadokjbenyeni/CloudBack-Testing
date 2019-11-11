using Akkatecture.Core;
using Akkatecture.ValueObjects;
using Newtonsoft.Json;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Entities
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class SubscriptionProcessingId : Identity<SubscriptionProcessingId>
    {
        public SubscriptionProcessingId(string value) : base(value)
        {

        }
    }
}
