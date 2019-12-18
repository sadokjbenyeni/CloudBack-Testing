using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;

namespace CloudBacktesting.PaymentService.Domain.Repositories.PaymentAccountRepository
{
    public class PaymentAccountReadModel : IReadModel, IAmReadModelFor<PaymentAccount, PaymentAccountId, PaymentAccountCreatedEvent>, IMongoDbReadModel
    {
        public string Id => throw new System.NotImplementedException();

        public long? Version { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Apply(IReadModelContext context, IDomainEvent<PaymentAccount, PaymentAccountId, PaymentAccountCreatedEvent> domainEvent)
        {
        }
    }
}
