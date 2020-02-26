using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Sagas.BillingCreation.Events;
using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Sagas.BillingCreation
{
    public class BillingCreationSaga : AggregateSaga<BillingCreationSaga, BillingCreationSagaId, BillingCreationSagaLocator>,
                                       ISagaIsStartedBy<BillingItem, BillingItemId, BillingItemCreatedEvent>,
                                       ISagaHandles<BillingItem, BillingItemId, SubscriptionNPaymentToBillingLinkedEvent>,
                                       ISagaHandles<PaymentMethod, PaymentMethodId, BillingItemLinkedEvent>
    {
        public BillingCreationSaga(BillingCreationSagaId id) : base(id) { }

        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, BillingItemCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new SubscriptionNPaymentLinkToBillingCommand(new BillingItemId(domainEvent.AggregateIdentity.Value),
                                                                             domainEvent.AggregateEvent.SubscriptionRequestId,
                                                                             domainEvent.AggregateEvent.PaymentMethodId,
                                                                             domainEvent.AggregateEvent.PaymentMethodStatus,
                                                                             domainEvent.AggregateEvent.SubscriptionType);
            this.Publish(command);
            this.Emit(new BillingIemLinkedSagaEvent(domainEvent.AggregateEvent.ItemId, domainEvent.AggregateEvent.PaymentMethodId, domainEvent.AggregateEvent.PaymentMethodStatus));
            return Task.CompletedTask;
        }
        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, SubscriptionNPaymentToBillingLinkedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new BillingItemLinkToPaymentMethodCommand(new PaymentMethodId(domainEvent.AggregateEvent.PaymentMethodId),
                                                                            domainEvent.AggregateIdentity.Value,
                                                                            domainEvent.AggregateEvent.SubscriptionType);
            this.Publish(command);
            return Task.CompletedTask;
        }
        public Task HandleAsync(IDomainEvent<PaymentMethod, PaymentMethodId, BillingItemLinkedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            ; if (domainEvent.AggregateEvent.PaymentMethodStatus == "Validated")
            {
                var validateCommand = new BillingItemSystemValidateCommand(domainEvent.AggregateEvent.ItemId, domainEvent.AggregateIdentity.Value);
                this.Publish(validateCommand);
            }
            else if (domainEvent.AggregateEvent.PaymentMethodStatus == "Declined")
            {
                var declineCommand = new BillingItemSystemDeclineCommand(domainEvent.AggregateEvent.ItemId, domainEvent.AggregateIdentity.Value);
                this.Publish(declineCommand);
            }
            this.Emit(new BillingCreationSagaCompletedEvent());
            return Task.CompletedTask;
        }

        public void Apply(BillingIemLinkedSagaEvent @event)
        {

        }
        public void Apply(BillingCreationSagaCompletedEvent @event)
        {
            Complete();
        }
    }
}
