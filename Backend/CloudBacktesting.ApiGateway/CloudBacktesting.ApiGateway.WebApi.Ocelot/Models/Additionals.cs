using Newtonsoft.Json;

namespace CloudBacktesting.ApiGateway.WebApi.Ocelot.Models
{

    public class Additionals
    {
        [JsonProperty("subscriptionaccountid",NullValueHandling =NullValueHandling.Ignore)]
        public string SubscriptionAccountId { get; set; }

        [JsonProperty("paymentaccountid", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentAccountId { get; set; }

    }
}
