using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository
{
    public class SubscriptionRequestReadModel : IReadModel, IAmReadModelFor<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent>, IMongoDbReadModel
    {
        public string Id { get; private set; }
        public string SubscriptionAccountId { get; set; }
        public long? Version { get; set; }
        public string Subscriber { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public DateTime SubscriptionDate { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            SubscriptionAccountId = domainEvent.AggregateEvent.SubscriptionAccountId;
            Subscriber = domainEvent.AggregateEvent.Subscriber;
            Status = domainEvent.AggregateEvent.Status;
            Type = domainEvent.AggregateEvent.Type;
            SubscriptionDate = DateTime.UtcNow;
        }
    }
}
