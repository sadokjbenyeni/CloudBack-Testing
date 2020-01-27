using Newtonsoft.Json;
using System.Collections.Generic;

namespace CloudBacktesting.ApiGateway.WebApi.Ocelot.Models
{
    public class UserReceivedData
    {
        [JsonProperty("firstname")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("roleName")]
        public IEnumerable<string> Role { get; set; }
        [JsonProperty("islogin")]
        public bool IsLogin { get; set; }

        [JsonProperty("subscriptionAccountId")]
        public string SubscriptionAccountId { get; set; }
    }
}
