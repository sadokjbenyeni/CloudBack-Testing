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
            var subscriptionId = domainEvent.Metadata["subscription-id"];
            var subscriptionSagaId = new SubscriptionCreationSagaId($"subscriptionsaga-{subscriptionId}");

            return await Task.FromResult<ISagaId>(subscriptionSagaId);
        }
    }
}
