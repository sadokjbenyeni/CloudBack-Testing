using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionConfiguration.Events;
using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionConfiguration
{
    public class SubscriptionConfigurationSaga : AggregateSaga<SubscriptionConfigurationSaga, SubscriptionConfigurationSagaId, SubscriptionConfigurationSagaLocator>,
                                                 ISagaIsStartedBy<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualConfiguredEvent>,
                                                 ISagaHandles<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestStatusUpdatedEvent>

    {
        public SubscriptionConfigurationSaga(SubscriptionConfigurationSagaId id) : base(id) { }

        public Task HandleAsync(IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualConfiguredEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new SubscriptionRequestManualConfigurationCommand(new SubscriptionRequestId(domainEvent.AggregateIdentity.Value), domainEvent.AggregateEvent.Subscriber);
            this.Publish(command);

            this.Emit(new SubscriptionRequestConfiguredEmailSentEvent(domainEvent.AggregateIdentity.Value, domainEvent.AggregateEvent.Message, domainEvent.AggregateEvent.SubscriptionAccountId, domainEvent.AggregateEvent.ActivatedDate));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestStatusUpdatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            Emit(new SubscriptionConfigurationSagaCompletedEvent());
            return Task.CompletedTask;
        }

        public void Apply(SubscriptionRequestConfiguredEmailSentEvent @event)
        {

        }
        public void Apply(SubscriptionConfigurationSagaCompletedEvent @event)
        {
            Complete();
        }
    }
}
