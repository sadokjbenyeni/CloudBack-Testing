using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository
{
    public class SubscriptionAccountReadModel : IReadModel, IAmReadModelFor<SubscriptionAccount, SubscriptionAccountId, SubscriptionAccountCreatedEvent>, IMongoDbReadModel
    {
        public string Id { get; private set; }
        public long? Version { get; set; }
        public string Subscriber { get; set; }
        public DateTime CreationDate { get; set; }
        public int OrderId { get; set; }


        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionAccount, SubscriptionAccountId, SubscriptionAccountCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            Subscriber = domainEvent.AggregateEvent.Subscriber;
            CreationDate = DateTime.UtcNow;
            OrderId = domainEvent.AggregateEvent.OrderId;
        }
    }
}
