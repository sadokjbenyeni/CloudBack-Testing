using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using System;

namespace CloudBacktesting.PaymentService.Domain.Repositories.BillingItemRepository
{
    public class BillingItemReadModel : IReadModel, IMongoDbReadModel
        , IAmReadModelFor<BillingItem, BillingItemId, BillingItemCreatedEvent>
    {
        public string Id { get; private set; }
        public string PaymentMethodId { get; set; }
        public long? Version { get ; set ; }
        public string InvoiceId { get; set; }
        public DateTime InvoiceDate  { get; set; }
        public string Method { get; set; }
        public string Client { get; set; }
        public string CardHolder { get; set; }
        public string Address { get; set; }
        public string Amount { get; set; }


        public void Apply(IReadModelContext context, IDomainEvent<BillingItem, BillingItemId, BillingItemCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            PaymentMethodId = domainEvent.AggregateEvent.PaymentMethodId;
        }
    }
}
