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
        , IAmReadModelFor<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestRejectedEvent>
        , IAmReadModelFor<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualValidatedEvent>
        , IAmReadModelFor<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualDeclinedEvent>
        , IAmReadModelFor<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualConfiguredEvent>
    {
        public string Id { get; private set; }
        public string SubscriptionAccountId { get; set; }
        public long? Version { get; set; }
        public string Subscriber { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int OrderId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsSystemValidated { get; set; } = null;
        public bool? IsManualValidated { get; set; } = null;
        public string DeclineMessage { get; set; }
        public DateTime? ValidatedOrDeclinedDate { get; set; } = null;
        public DateTime? RejectedDate { get; set; } = null;
        public bool IsManualConfigured { get; set; } = false;
        public string ActivationMessage { get; set; }
        public DateTime? ActivatedDate { get; set; } = null;


        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            SubscriptionAccountId = domainEvent.AggregateEvent.SubscriptionAccountId;
            Status = domainEvent.AggregateEvent.Status;
            Type = domainEvent.AggregateEvent.Type;
            CreationDate = domainEvent.AggregateEvent.CreationDate;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionAccountAffectedEvent> domainEvent)
        {
            this.Subscriber = domainEvent.AggregateEvent.Subscriber;
            OrderId = domainEvent.AggregateEvent.OrderId;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestStatusUpdatedEvent> domainEvent)
        {
            this.Status = domainEvent.AggregateEvent.Status;
        }
        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestValidatedEvent> domainEvent)
        {
            this.IsSystemValidated = true;
        }
        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestRejectedEvent> domainEvent)
        {
            this.Subscriber = domainEvent.AggregateEvent.Subscriber;
            this.DeclineMessage = domainEvent.AggregateEvent.Message;
            this.IsSystemValidated = false;
            this.RejectedDate = domainEvent.AggregateEvent.SystemRejectedDate;
            OrderId = domainEvent.AggregateEvent.OrderId;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualValidatedEvent> domainEvent)
        {
            this.IsManualValidated = true;
            this.ValidatedOrDeclinedDate = domainEvent.AggregateEvent.ManualValidatedDate;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualDeclinedEvent> domainEvent)
        {
            this.IsManualValidated = false;
            this.DeclineMessage = domainEvent.AggregateEvent.Message;
            this.ValidatedOrDeclinedDate = domainEvent.AggregateEvent.ManualDeclinedDate;
        }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualConfiguredEvent> domainEvent)
        {
            this.IsManualConfigured = true;
            this.ActivatedDate = domainEvent.AggregateEvent.ActivatedDate;
            this.ActivationMessage = domainEvent.AggregateEvent.Message;
        }
    }
}
