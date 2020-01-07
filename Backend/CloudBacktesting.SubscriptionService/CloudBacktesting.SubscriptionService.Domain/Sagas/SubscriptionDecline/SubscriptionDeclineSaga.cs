using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionDecline.Event
{
    public class SubscriptionDeclineSaga : AggregateSaga<SubscriptionDeclineSaga, SubscriptionDeclineSagaId, SubscriptionDeclineSagaLocator>,
                                            ISagaIsStartedBy<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualDeclinedEvent>,
                                            ISagaHandles<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestStatusUpdatedEvent>
    {
        public SubscriptionDeclineSaga(SubscriptionDeclineSagaId id) : base(id)
        {
        }

        public Task HandleAsync(IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualDeclinedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new SubscriptionRequestManualDeclineSuccessCommand(domainEvent.AggregateIdentity.Value, domainEvent.AggregateEvent.Message);
            this.Publish(command);

            this.Emit(new SubscriptionRequestDeclinedEmailSentEvent(domainEvent.AggregateIdentity.Value, domainEvent.AggregateEvent.Message, domainEvent.AggregateEvent.SubscriptionAccountId, domainEvent.AggregateEvent.ManualDeclinedDate));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestStatusUpdatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            Emit(new SubscriptionDeclineSagaCompletedEvent());
            return Task.CompletedTask;
        }

        public void Apply(SubscriptionRequestDeclinedEmailSentEvent @event)
        {

        }
        public void Apply(SubscriptionDeclineSagaCompletedEvent @event)
        {
            Complete();
        }
    }
}
