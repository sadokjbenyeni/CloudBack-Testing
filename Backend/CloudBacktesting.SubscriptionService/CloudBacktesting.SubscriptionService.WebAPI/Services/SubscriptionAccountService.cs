using CloudBacktesting.SubscriptionService.WebAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Services
{
    public class SubscriptionAccountService
    {
        private readonly IMongoCollection<SubscriptionAccount> _subscriptions;

        public SubscriptionAccountService(ISubscriptionDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _subscriptions = database.GetCollection<SubscriptionAccount>(settings.SubscriptionsCollectionName);
        }

        public List<SubscriptionAccount> Get() => _subscriptions.Find(subscription => true).ToList();
        public SubscriptionAccount Get(string id) => _subscriptions.Find(subscription => subscription.Id == id).FirstOrDefault();

        public SubscriptionAccount Create(SubscriptionAccount subscription)
        {
            _subscriptions.InsertOne(subscription);
            return subscription;
        }

        public void Update(string id, SubscriptionAccount subscriptionIn) => _subscriptions.ReplaceOne(subscription => subscription.Id == id, subscriptionIn);

        public void Remove(SubscriptionAccount subscriptionIn) => _subscriptions.DeleteOne(subscription => subscription.Id == subscriptionIn.Id);

        public void Remove(string id) => _subscriptions.DeleteOne(subscription => subscription.Id == id);

    }
}
