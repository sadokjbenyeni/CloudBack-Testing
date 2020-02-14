using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.RabbitMQ.EventManager.Models
{
    [JsonObject]
    public class BillingItemRabbitMQDto
    {
        [JsonProperty(propertyName: "paymentmethodid")]
        public string PaymentMethodId { get; set; }
        [JsonProperty(propertyName: "subscriptionrequestid")]
        public string SubscriptionRequestId { get; set; }
        [JsonProperty(propertyName: "paymentaccountid")]
        public string PaymentAccountId { get; set; }
        [JsonProperty(propertyName: "type")]
        public string Type { get; set; }

    }
}
