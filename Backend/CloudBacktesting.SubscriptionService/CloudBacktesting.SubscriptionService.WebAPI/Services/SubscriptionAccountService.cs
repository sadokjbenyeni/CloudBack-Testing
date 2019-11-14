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
        private readonly IMongoCollection<SubscriptionAccountDto> _subscriptions;

        public SubscriptionAccountService(ISubscriptionDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _subscriptions = database.GetCollection<SubscriptionAccountDto>(settings.SubscriptionsCollectionName);
        }

        public List<SubscriptionAccountDto> Get() => _subscriptions.Find(subscription => true).ToList();
        public SubscriptionAccountDto Get(string id) => _subscriptions.Find(subscription => subscription.Id == id).FirstOrDefault();

        public SubscriptionAccountDto Create(SubscriptionAccountDto subscription)
        {
            _subscriptions.InsertOne(subscription);
            return subscription;
        }

        public void Update(string id, SubscriptionAccountDto subscriptionIn) => _subscriptions.ReplaceOne(subscription => subscription.Id == id, subscriptionIn);

        public void Remove(SubscriptionAccountDto subscriptionIn) => _subscriptions.DeleteOne(subscription => subscription.Id == subscriptionIn.Id);

        public void Remove(string id) => _subscriptions.DeleteOne(subscription => subscription.Id == id);

    }
}
