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
        , IAmReadModelFor<BillingItem, BillingItemId, BillingItemToPaymentMethodLinkedEvent>
        , IAmReadModelFor<BillingItem, BillingItemId, SubscriptionRequestToBillingItemLinkedEvent>
        //, IAmReadModelFor<BillingItem, BillingItemId, PaymentExecutedEvent>
        , IAmReadModelFor<BillingItem, BillingItemId, InvoiceGeneratedEvent>
    {
        public string Id { get; private set; }
        public string PaymentMethodId { get; set; }
        public string SubscriptionRequestId { get; set; }
        public long? Version { get ; set ; }
        public string InvoiceId { get; set; }
        public DateTime InvoiceDate  { get; set; }
        public string Method { get; set; }
        public string Client { get; set; }
        public string CardHolder { get; set; }
        public string Address { get; set; }
        public DateTime CreationDate { get; set; }
        public string MerchantTransactionId { get; set; }
        public string Subscriber { get; set; }
        public Card CardDetails { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<BillingItem, BillingItemId, BillingItemCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
        }

        public void Apply(IReadModelContext context, IDomainEvent<BillingItem, BillingItemId, InvoiceGeneratedEvent> domainEvent)
        {
            InvoiceId = domainEvent.AggregateEvent.InvoiceId;
            InvoiceDate = domainEvent.AggregateEvent.InvoiceDate;
            Method = domainEvent.AggregateEvent.Method;
            Client = domainEvent.AggregateEvent.Client;
            CardHolder = domainEvent.AggregateEvent.CardHolder;
            Address = domainEvent.AggregateEvent.Address;
        }

        public void Apply(IReadModelContext context, IDomainEvent<BillingItem, BillingItemId, BillingItemToPaymentMethodLinkedEvent> domainEvent)
        {
            PaymentMethodId = domainEvent.AggregateEvent.PaymentMethodId;
        }

        public void Apply(IReadModelContext context, IDomainEvent<BillingItem, BillingItemId, SubscriptionRequestToBillingItemLinkedEvent> domainEvent)
        {
            SubscriptionRequestId = domainEvent.AggregateEvent.SubscriptionRequestId;
        }

        //public void Apply(IReadModelContext context, IDomainEvent<BillingItem, BillingItemId, PaymentExecutedEvent> domainEvent)
        //{
        //    MerchantTransactionId = domainEvent.AggregateEvent.MerchantTransactionId;
        //    Subscriber = domainEvent.AggregateEvent.Subscriber;
        //    CardDetails = domainEvent.AggregateEvent.CardDetails;
        //    Amount = domainEvent.AggregateEvent.Amount;
        //    Currency = domainEvent.AggregateEvent.Currency;
        //}
    }
}
