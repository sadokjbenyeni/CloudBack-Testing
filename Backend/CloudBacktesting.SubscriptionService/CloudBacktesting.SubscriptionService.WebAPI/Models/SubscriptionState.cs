using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SubscriptionState
    {
        PendingValidation,PendingConfiguration,Active,All,Error,Unsubscribed
    }
}
