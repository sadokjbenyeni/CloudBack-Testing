using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using Newtonsoft.Json;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models
{
    public class SubscriptionAccountDto
    {
        public string Id { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
        
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        
        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Date")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("IsValid")]
        public bool IsValid { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set;}
    }

}
