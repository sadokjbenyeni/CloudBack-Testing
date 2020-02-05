using Newtonsoft.Json;

namespace CloudBacktesting.SubscriptionService.RabbitMQ.EventManager.Models
{
    [JsonObject]
    public class SubscriptionAccountRabbitMQDto
    {
        [JsonProperty(propertyName: "subscriptionAccountId")]
        public string Id { get; set; }
        [JsonProperty(propertyName: "email")]
        public string User { get; set; }
    }
}
