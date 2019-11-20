using Newtonsoft.Json;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models
{
    public class CreateSubscriptionCommandDto
    {
        [JsonProperty("subscriptionType")]
        public string SubscriptionType { get; }
    }
}
