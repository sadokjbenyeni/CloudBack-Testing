using CloudBacktesting.SubscriptionService.Infra.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Infra.Services
{
    public class SubscriptionServiceMongo
    {
        private readonly IMongoCollection<Subscription> subscriptions;

        public SubscriptionServiceMongo(ISubscriptionDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            subscriptions = database.GetCollection<Subscription>(settings.SubscriptionCollectionName);
        }

        public List<Subscription> Get() =>
            subscriptions.Find(subscription => true).ToList();

        public Subscription Get(string id) =>
            subscriptions.Find(subscription => subscription.SubscriptionAccountId == id).FirstOrDefault();

        public Subscription Create(Subscription subscription)
        {
            subscriptions.InsertOne(subscription);
            return subscription;
        }

        public void Update(string id, Subscription subscriptionIn) =>
            subscriptions.ReplaceOne(subscription => subscription.SubscriptionAccountId == id, subscriptionIn);

        public void Remove(Subscription subscriptionIn) =>
            subscriptions.DeleteOne(subscription => subscription.SubscriptionAccountId == subscriptionIn.Id);

        public void Remove(string id) =>
            subscriptions.DeleteOne(subscription => subscription.SubscriptionAccountId == id);
    }
}
