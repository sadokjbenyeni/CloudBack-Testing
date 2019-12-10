using EventFlow.Aggregates;
using EventFlow.Sagas;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionDecline.Event
{
    public class SubscriptionDeclineSagaLocator : ISagaLocator
    {
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            if (!(domainEvent.GetAggregateEvent() is ISubscriptionSagaRequestId requestId))
            {
                throw new ArgumentException("cannot build the saga identifier");
            }
            return Task.FromResult<ISagaId>(new SubscriptionDeclineSagaId($"subscriptiondeclinesaga-{requestId.RequestId.Remove(0, 20)}"));
        }
    }
}
