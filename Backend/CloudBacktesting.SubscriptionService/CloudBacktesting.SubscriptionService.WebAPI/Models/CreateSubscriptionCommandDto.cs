using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models
{
    public class CreateSubscriptionCommandDto
    {
        [JsonProperty("subscriptionType")]
        public string SubscriptionType { get; }
    }
}
