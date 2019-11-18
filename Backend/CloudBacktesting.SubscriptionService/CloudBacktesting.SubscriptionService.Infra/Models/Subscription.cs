using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;


namespace CloudBacktesting.SubscriptionService.Infra.Models
{
    public class Subscription
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Subscription Account Id")]
        public string SubscriptionAccountId { get; set; }
        public string UserIdentifier { get; set; }
        public double ElapsedTimeToCreation { get; }
        public DateTime CreationDate { get; set; }
        public string ValidatorUserIdentifier { get; set; }
        public string EnvironmentSetupUserIdentifier { get; set; }
        public string TypeOfSubscription { get; set; }
        public string Status { get; set; }
    }

}
