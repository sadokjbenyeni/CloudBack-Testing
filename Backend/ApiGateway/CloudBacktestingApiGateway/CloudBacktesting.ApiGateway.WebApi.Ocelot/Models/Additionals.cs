using Newtonsoft.Json;

namespace CloudBacktesting.ApiGateway.WebApi.Ocelot.Models
{

    public class Additionals
    {
        [JsonProperty("subscriptionaccountid",NullValueHandling =NullValueHandling.Ignore)]
        public string Subscriptionaccountid { get; set; }
    }
}
