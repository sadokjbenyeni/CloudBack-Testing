using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events;
using EventFlow.Aggregates;
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
        , IAmReadModelFor<PaymentMethod, PaymentMethodId, PaymentMethodStatusUpdatedEvent>
        , IAmReadModelFor<PaymentMethod, PaymentMethodId, PaymentMethodValidatedEvent>
    {
        public string Id { get; set; }
        public string PaymentAccountId { get; set; }
        public long? Version { get; set; }
        public string Status { get; set; }
        public string CardNumber { get; set; }
        public string Client { get; set; }
        public string CardType { get; set; }
        public string CardHolder { get; set; }
        public string Cryptogram { get; set; }
        public string ExpirationYear { get; set; }
        public string ExpirationMonth { get; set; }


        public void Apply(IReadModelContext context, EventFlow.Aggregates.IDomainEvent<PaymentMethod, PaymentMethodId, PaymentMethodCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            PaymentAccountId = domainEvent.AggregateEvent.PaymentAccountId;
            Status = domainEvent.AggregateEvent.Status;
            CardNumber = domainEvent.AggregateEvent.CardNumber;
            CardHolder = domainEvent.AggregateEvent.CardHolder;
            CardType = domainEvent.AggregateEvent.CardType;
            Cryptogram = domainEvent.AggregateEvent.Cryptogram;
            ExpirationYear = domainEvent.AggregateEvent.ExpirationYear;
            ExpirationMonth = domainEvent.AggregateEvent.ExpirationMonth;
        }

        public void Apply(IReadModelContext context, EventFlow.Aggregates.IDomainEvent<PaymentMethod, PaymentMethodId, PaymentAccountAffectedEvent> domainEvent)
        {
            Client = domainEvent.AggregateEvent.Client;
        }

        public void Apply(IReadModelContext context, EventFlow.Aggregates.IDomainEvent<PaymentMethod, PaymentMethodId, PaymentMethodValidatedEvent> domainEvent)
        {
            Id = domainEvent.AggregateEvent.MethodId;
        }

        public void Apply(IReadModelContext context, IDomainEvent<PaymentMethod, PaymentMethodId, PaymentMethodStatusUpdatedEvent> domainEvent)
        {
            Status = domainEvent.AggregateEvent.Status;
        }
    }
}
