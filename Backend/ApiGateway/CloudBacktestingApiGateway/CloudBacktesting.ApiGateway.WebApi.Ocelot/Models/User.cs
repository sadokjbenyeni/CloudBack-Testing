using Newtonsoft.Json;
using System.Collections.Generic;

namespace CloudBacktesting.ApiGateway.WebApi.Ocelot.Models
{
    public class User
    {

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("roleName")]
        public IEnumerable<string> Role { get; set; }
        [JsonProperty("islogin")]
        public bool IsLogin { get; set; }
        [JsonProperty("additionals")]
        public Dictionary<string,string> Additionnals { get; }
    }
}
