using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository
{
    public class SubscriptionRequestReadModel : IReadModel, IMongoDbReadModel
        , IAmReadModelFor<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent>
        , IAmReadModelFor<SubscriptionRequest, SubscriptionRequestId, SubscriptionAccountAffectedEvent>
        , IAmReadModelFor<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestStatusUpdatedEvent>
        , IAmReadModelFor<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestValidatedEvent>
    {
        public string Id { get; private set; }
        public string SubscriptionAccountId { get; set; }
        public string RequestId { get; set; }
        public long? Version { get; set; }
        public string Subscriber { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSystemValidated { get; set; } = false;

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            SubscriptionAccountId = domainEvent.AggregateEvent.SubscriptionAccountId;
            Status = domainEvent.AggregateEvent.Status;
            Type = domainEvent.AggregateEvent.Type;
            CreationDate = DateTime.UtcNow;
            RequestId = domainEvent.AggregateEvent.RequestId;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionAccountAffectedEvent> domainEvent)
        {
            this.Subscriber = domainEvent.AggregateEvent.Subscriber;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestStatusUpdatedEvent> domainEvent)
        {
            this.Status = domainEvent.AggregateEvent.Status;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestValidatedEvent> domainEvent)
        {
            this.IsSystemValidated = true; 
        }
    }
}
