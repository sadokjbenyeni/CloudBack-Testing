using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Events;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas
{
    public class SubscriptionCreationSaga : AggregateSaga<SubscriptionCreationSaga, SubscriptionCreationSagaId, SubscriptionCreationSagaLocator>,
                                            ISagaIsStartedBy<SubscriptionAccount, SubscriptionAccountId, SubscriptionAccountCreatedEvent>,
                                            ISagaHandles<SubscriptionRequestId, SubscriptionRequestId, SubscriptionRequestCreatedEvent>

    {
        public async Task HandleAsync(IDomainEvent<SubscriptionAccount, SubscriptionAccountId, SubscriptionAccountCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            await Emit(new SubscriptionAccountCreatedEvent(domainEvent.AggregateEvent.Subscriber, domainEvent.AggregateEvent.SubscriptionDate));

            Publish(new SubscriptionAccountCreationCommand(SubscriptionAccountId.New, domainEvent.AggregateEvent.Subscriber, domainEvent.AggregateEvent.SubscriptionDate));
        }

        public void Apply(SubscriptionAccountCreatedEvent aggregateEvent) { }

        public async Task HandleAsync(IDomainEvent<SubscriptionRequestId, SubscriptionRequestId, SubscriptionRequestCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            await Emit(new SubscriptionRequestCreatedEvent(domainEvent.AggregateEvent.Status, domainEvent.AggregateEvent.Subscriber, domainEvent.AggregateEvent.Type, domainEvent.AggregateEvent.SubscriptionDate));
        }

        public void Apply(SubscriptionRequestCreatedEvent aggregateEvent)
        {
            Complete();
        }

    }
}
