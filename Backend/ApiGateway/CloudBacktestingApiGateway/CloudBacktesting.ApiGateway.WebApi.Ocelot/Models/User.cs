using Newtonsoft.Json;
using System.Collections.Generic;

namespace CloudBacktesting.ApiGateway.WebApi.Ocelot.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("companyType")]
        public string CompanyType { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("state")]
        public bool State { get; set; }

        [JsonProperty("roleName")]
        public IEnumerable<string> RoleName { get; set; }
    }
}
