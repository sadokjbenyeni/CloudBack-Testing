using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionDecline.Event;
using EventFlow.Aggregates;
using EventFlow.Sagas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionConfiguration
{
    public class SubscriptionConfigurationSagaLocator : ISagaLocator
    {
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            if (!(domainEvent.GetAggregateEvent() is ISubscriptionSagaRequestId requestId))
            {
                throw new ArgumentException("cannot build the saga identifier");
            }
            return Task.FromResult<ISagaId>(new SubscriptionConfigurationSagaId($"subscriptionconfigurationsaga-{requestId.RequestId.Remove(0, 20)}"));
        }
    }
}
