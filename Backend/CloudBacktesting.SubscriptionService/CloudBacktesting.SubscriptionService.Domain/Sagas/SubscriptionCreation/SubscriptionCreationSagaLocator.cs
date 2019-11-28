using EventFlow.Aggregates;
using EventFlow.Sagas;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionCreation
{
    public class SubscriptionCreationSagaLocator : ISagaLocator
    {
        public async Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            //var subscriptionId = domainEvent.Metadata["subscription-id"];
            //var subscriptionId = domainEvent.Metadata.AggregateId;
            //var subscriptionSagaId = new SubscriptionCreationSagaId($"subscriptionsaga-{subscriptionId}");
            var subscriptionSagaId = SubscriptionCreationSagaId.New;
            return await Task.FromResult<ISagaId>(subscriptionSagaId);
        }
    }
}
