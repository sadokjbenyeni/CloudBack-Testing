using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;

namespace CloudBacktesting.PaymentService.Domain.Repositories.BillingItemRepository
{
    public class BillingItemReadModel : IReadModel, IMongoDbReadModel
        , IAmReadModelFor<BillingItem, BillingItemId, BillingItemCreatedEvent>
    {
        public string Id { get; private set; }
        public string PaymentMethodId { get; set; }
        public long? Version { get ; set ; }

        public void Apply(IReadModelContext context, IDomainEvent<BillingItem, BillingItemId, BillingItemCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            PaymentMethodId = domainEvent.AggregateEvent.PaymentMethodId;
        }
    }
}
