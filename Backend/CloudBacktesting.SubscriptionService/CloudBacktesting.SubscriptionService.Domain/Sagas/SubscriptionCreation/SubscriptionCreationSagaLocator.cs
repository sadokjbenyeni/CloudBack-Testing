using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.Sagas;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionCreation
{
    public class SubscriptionCreationSagaLocator : ISagaLocator
    {
      
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            if(!(domainEvent.GetAggregateEvent() is ISubscriptionSagaRequestId requestId))
            {
                throw new ArgumentException("cannot build the saga identifier");
            }
            return Task.FromResult<ISagaId>(new SubscriptionCreationSagaId($"subscriptioncreationsaga-{requestId.RequestId.Remove(0, 20)}"));            
        }
    }
}
