using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository
{
    public class SubscriptionRequestReadModelAdmin : IReadModel, IMongoDbReadModel
        , IAmReadModelFor<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent>
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
        public bool IsAdminValidated { get; set; } = false;


        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            SubscriptionAccountId = domainEvent.AggregateEvent.SubscriptionAccountId;
            Status = domainEvent.AggregateEvent.Status;
            Type = domainEvent.AggregateEvent.Type;
            CreationDate = DateTime.UtcNow;
            RequestId = domainEvent.AggregateEvent.RequestId;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestStatusUpdatedEvent> domainEvent)
        {
            this.Status = domainEvent.AggregateEvent.Status;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestValidatedEvent> domainEvent)
        {
            this.IsSystemValidated = true;
        }
        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestValidatedAdminEvent> domainEvent)
        {
            this.IsAdminValidated = true;
        }
    }
}
