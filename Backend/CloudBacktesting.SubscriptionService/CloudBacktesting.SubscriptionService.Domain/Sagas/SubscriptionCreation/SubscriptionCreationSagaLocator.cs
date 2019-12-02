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
        private static Dictionary<string, ISagaId> _mapping = new Dictionary<string, ISagaId>();
        
        public async Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            if (domainEvent is IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent> startedEvent)
            {
                return await Task.FromResult(CreateNewSagaId(startedEvent));
            }
            return _mapping.TryGetValue(domainEvent.GetIdentity().Value, out var sagaId) ? sagaId : throw new ArgumentOutOfRangeException($"The aggregate is not affected at the saga");
            //var subscriptionId = domainEvent.Metadata["subscription-id"];
            //var subscriptionId = domainEvent.Metadata.AggregateId;
            //var subscriptionSagaId = new SubscriptionCreationSagaId($"subscriptionsaga-{subscriptionId}");
            
        }

        private ISagaId CreateNewSagaId(IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent> startedEvent)
        {
            var subscriptionSagaId = SubscriptionCreationSagaId.New;
            _mapping.Add(startedEvent.AggregateEvent.SubscriptionAccountId, subscriptionSagaId);
            _mapping.Add(startedEvent.AggregateIdentity.Value, subscriptionSagaId);
            return subscriptionSagaId;
        }
    }
}
