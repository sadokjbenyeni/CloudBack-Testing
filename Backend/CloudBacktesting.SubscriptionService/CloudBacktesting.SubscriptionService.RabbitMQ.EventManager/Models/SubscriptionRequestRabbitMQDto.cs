using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.RabbitMQ.EventManager.Models
{
    [JsonObject]
    public class SubscriptionRequestRabbitMQDto
    {
        [JsonProperty(propertyName: "subscriptionRequestId")]
        public string Id { get; set; }

    }
}
