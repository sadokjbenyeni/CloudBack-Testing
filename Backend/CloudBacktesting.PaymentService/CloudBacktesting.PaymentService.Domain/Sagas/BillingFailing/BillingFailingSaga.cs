using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Sagas.BillingFailing.Events;
using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Sagas.BillingFailing
{
    public class BillingFailingSaga : AggregateSaga<BillingFailingSaga, BillingFailingSagaId, BillingFailingSagaLocator>,
                                      ISagaIsStartedBy<BillingItem, BillingItemId, PaymentFailedEvent>,
                                      ISagaHandles<BillingItem, BillingItemId, BillingItemStatusUpdatedEvent>
    {
        public BillingFailingSaga(BillingFailingSagaId id) : base(id) { }
        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, PaymentFailedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new PaymentExecutionFailureCommand(domainEvent.AggregateIdentity.Value, domainEvent.AggregateEvent.PaymentMethodId);
            this.Publish(command);
            this.Emit(new BillingItemFailedEmailSentEvent(domainEvent.AggregateIdentity.Value, domainEvent.AggregateEvent.PaymentMethodId, domainEvent.AggregateEvent.Message, domainEvent.AggregateEvent.FailureDate));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, BillingItemStatusUpdatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            Emit(new BillingFailingSagaCompletedEvent());
            return Task.CompletedTask;
        }

        public void Apply(BillingItemFailedEmailSentEvent @event)
        {

        }
        public void Apply(BillingFailingSagaCompletedEvent @event)
        {
            Complete();
        }
    }
}
