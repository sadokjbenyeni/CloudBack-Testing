using Newtonsoft.Json;

namespace CloudBacktesting.PaymentService.RabbitMQ.EventManager.Models
{
    [JsonObject]

    class PaymentAccountRabbitMQDto
    {
        [JsonProperty(propertyName: "paymentAccountId")]
        public string Id { get; set; }
        [JsonProperty(propertyName: "email")]
        public string User { get; set; }

    }
}
