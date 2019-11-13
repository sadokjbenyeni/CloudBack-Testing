using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using Newtonsoft.Json;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models
{
    public class SubscriptionAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonProperty("Status")]
        [BsonElement("Status")]
        public SubscriptionStatus Status { get; set; }

        [JsonProperty("User")]
        [BsonElement("User")]
        public SubscriptionUser User { get; set; }

        [JsonProperty("Type")]
        [BsonElement("Type")]
        public SubscriptionType Type { get; set; }

        [JsonProperty("Date")]
        [BsonElement("Date")]
        public DateTime Date { get; set; }

        public SubscriptionValidation Validation { get; set; }
    }

    public class SubscriptionDatabaseSettings : ISubscriptionDatabaseSettings
    {
        public string SubscriptionsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISubscriptionDatabaseSettings
    {
        string SubscriptionsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
