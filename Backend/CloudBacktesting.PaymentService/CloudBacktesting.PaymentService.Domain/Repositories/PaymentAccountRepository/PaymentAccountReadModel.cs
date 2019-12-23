using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using System;

namespace CloudBacktesting.PaymentService.Domain.Repositories.PaymentAccountRepository
{
    public class PaymentAccountReadModel : IReadModel, IMongoDbReadModel
        , IAmReadModelFor<PaymentAccount, PaymentAccountId, PaymentAccountCreatedEvent>
    {
        public string Id { get; private set; }
        public long? Version { get; set; }
        public string Client { get; set; }
        public DateTime CreationDate { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<PaymentAccount, PaymentAccountId, PaymentAccountCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            Client = domainEvent.AggregateEvent.Client;
            CreationDate = DateTime.UtcNow;
        }
    }
}
