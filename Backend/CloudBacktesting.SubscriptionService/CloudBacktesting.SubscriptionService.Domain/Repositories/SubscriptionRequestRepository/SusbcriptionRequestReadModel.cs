using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository
{
    public class SusbcriptionRequestReadModel : IReadModel, IAmReadModelFor<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent>
    {
        public string Id { get; private set; }
        public long? Version { get; }
        public string Subscriber { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public DateTime SubscriptionDate { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            Subscriber = domainEvent.AggregateEvent.Subscriber;
            Status = domainEvent.AggregateEvent.Status;
            Type = domainEvent.AggregateEvent.Type;
            SubscriptionDate = domainEvent.AggregateEvent.SubscriptionDate;
        }
    }
}
