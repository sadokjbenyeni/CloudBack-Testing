using Newtonsoft.Json;
using System.Collections.Generic;

namespace CloudBacktesting.ApiGateway.WebApi.Ocelot.Models
{
    public class UserTokenFormat
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("State")]
        public int State { get; set; }

        [JsonProperty("Roles")]
        public IEnumerable<string> Role { get; set; }
        [JsonProperty("Islogin")]
        public bool IsLogin { get; set; }
        [JsonProperty("Additionals",NullValueHandling=NullValueHandling.Ignore)]
        public Additionals Additionals { get; set; }
    }
}
