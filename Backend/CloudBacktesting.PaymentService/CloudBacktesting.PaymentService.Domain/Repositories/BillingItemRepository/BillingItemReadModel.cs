using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using System;

namespace CloudBacktesting.PaymentService.Domain.Repositories.BillingItemRepository
{
    public class BillingItemReadModel : IReadModel, IMongoDbReadModel
        , IAmReadModelFor<BillingItem, BillingItemId, BillingItemCreatedEvent>
        , IAmReadModelFor<BillingItem, BillingItemId, SubscriptionNPaymentToBillingLinkedEvent>
        , IAmReadModelFor<BillingItem, BillingItemId, PaymentExecutedEvent>
        , IAmReadModelFor<BillingItem, BillingItemId, InvoiceGeneratedEvent>

    {
        public string Id { get; private set; }
        public string PaymentMethodId { get; set; }
        public string SubscriptionRequestId { get; set; }
        public long? Version { get ; set ; }
        public string InvoiceId { get; set; }
        public DateTime InvoiceDate  { get; set; }
        public string CardHolder { get; set; }
        public string Address { get; set; }
        public DateTime CreationDate { get; set; }
        public string Subscriber { get; set; }
        public Card CardDetails { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<BillingItem, BillingItemId, BillingItemCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            CreationDate = domainEvent.AggregateEvent.CreateDate;
        }

        public void Apply(IReadModelContext context, IDomainEvent<BillingItem, BillingItemId, SubscriptionNPaymentToBillingLinkedEvent> domainEvent)
        {
            SubscriptionRequestId = domainEvent.AggregateEvent.SubscriptionRequestId;
            PaymentMethodId = domainEvent.AggregateEvent.PaymentMethodId;
            Type = domainEvent.AggregateEvent.SubscriptionType;
        }

        public void Apply(IReadModelContext context, IDomainEvent<BillingItem, BillingItemId, InvoiceGeneratedEvent> domainEvent)
        {
            InvoiceDate = domainEvent.AggregateEvent.InvoiceDate;
        }

        public void Apply(IReadModelContext context, IDomainEvent<BillingItem, BillingItemId, PaymentExecutedEvent> domainEvent)
        {
            InvoiceId = domainEvent.AggregateEvent.MerchantTransactionId;
            Subscriber = domainEvent.AggregateEvent.Subscriber;
            CardHolder = domainEvent.AggregateEvent.CardDetails.HolderName;
            CardDetails = domainEvent.AggregateEvent.CardDetails;
            Currency = domainEvent.AggregateEvent.Currency;
        }
    }
}
