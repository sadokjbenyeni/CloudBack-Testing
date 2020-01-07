using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.RabbitMQ.EventManager.Models
{
    [JsonObject]
    public class SubscriptionAccountRabbitMQDto
    {
        [JsonProperty(propertyName: "subscriptionAccountId")]
        public string Id { get; set; }
        [JsonProperty(propertyName: "subscriber")]
        public string Subscriber { get; set; }
    }
}
