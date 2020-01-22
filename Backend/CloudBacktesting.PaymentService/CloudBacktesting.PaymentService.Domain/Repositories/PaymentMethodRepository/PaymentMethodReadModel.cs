using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Repositories.PaymentMethodRepository
{
    public class PaymentMethodReadModel : IReadModel, IMongoDbReadModel
        , IAmReadModelFor<PaymentMethod, PaymentMethodId, PaymentMethodCreatedEvent>
        , IAmReadModelFor<PaymentMethod, PaymentMethodId, PaymentAccountAffectedEvent>
        , IAmReadModelFor<PaymentMethod, PaymentMethodId, PaymentMethodValidatedEvent>
    {
        public string Id { get; set; }
        public string PaymentAccountId { get; set; }
        public long? Version { get; set; }
        public string CardNumber { get; set; }
        public string Client { get; set; }
        public string CardType { get; set; }
        public string CardHolder { get; set; }
        public string ExpirationDate { get; set; }



        public void Apply(IReadModelContext context, EventFlow.Aggregates.IDomainEvent<PaymentMethod, PaymentMethodId, PaymentMethodCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            PaymentAccountId = domainEvent.AggregateEvent.PaymentAccountId;
            CardNumber = domainEvent.AggregateEvent.CardNumber;
            CardHolder = domainEvent.AggregateEvent.CardHolder;
            CardType = domainEvent.AggregateEvent.CardType;
            ExpirationDate = domainEvent.AggregateEvent.ExpirationDate.ToString();
        }

        public void Apply(IReadModelContext context, EventFlow.Aggregates.IDomainEvent<PaymentMethod, PaymentMethodId, PaymentAccountAffectedEvent> domainEvent)
        {
            Client = domainEvent.AggregateEvent.Client;
        }

        public void Apply(IReadModelContext context, EventFlow.Aggregates.IDomainEvent<PaymentMethod, PaymentMethodId, PaymentMethodValidatedEvent> domainEvent) 
        {
        }
    }
}
