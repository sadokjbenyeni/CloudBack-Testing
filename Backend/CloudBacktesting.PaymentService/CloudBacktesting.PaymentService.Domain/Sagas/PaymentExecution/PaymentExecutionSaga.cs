using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Sagas.PaymentExecution.Events;
using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentExecution
{
    public class PaymentExecutionSaga : AggregateSaga<PaymentExecutionSaga, PaymentExecutionSagaId, PaymentExecutionSagaLocator>,
                                        ISagaIsStartedBy<BillingItem, BillingItemId, PaymentExecutionInitializedEvent>,
                                        ISagaHandles<PaymentMethod, PaymentMethodId, BillingItemLinkedEvent>,
                                        ISagaHandles<PaymentMethod, PaymentMethodId, PaymentExecutedEvent>,
                                        ISagaHandles<BillingItem, BillingItemId, InvoiceGeneratedEvent>
    {
        public PaymentExecutionSaga(PaymentExecutionSagaId id) : base(id) { }

        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, PaymentExecutionInitializedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new PaymentMethodLinkToBillingItemCommand(new PaymentMethodId(domainEvent.AggregateEvent.PaymentMethodId), domainEvent.AggregateEvent.ItemId, domainEvent.AggregateEvent.MerchantTransactionId, domainEvent.AggregateEvent.Type);
            this.Publish(command);
            this.Emit(new PaymentInitializedSagaEvent(domainEvent.AggregateIdentity.Value, domainEvent.AggregateEvent.MerchantTransactionId));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<PaymentMethod, PaymentMethodId, BillingItemLinkedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new PaymentExecutionCommand(domainEvent.AggregateIdentity, domainEvent.AggregateEvent.MerchantTransactionId,domainEvent.AggregateEvent.ItemId, domainEvent.AggregateEvent.SubscriptionRequestId, domainEvent.AggregateEvent.CardDetails, domainEvent.AggregateEvent.Type, domainEvent.AggregateEvent.Subscriber);
            this.Publish(command);

            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<PaymentMethod, PaymentMethodId, PaymentExecutedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            if (!domainEvent.AggregateEvent.IsPaymentSuccessful)
            {
                var failCommand = new PaymentExecutionFailureCommand(domainEvent.AggregateIdentity.Value, domainEvent.AggregateEvent.PaymentMethodId);
                this.Publish(failCommand);
            }

            var command = new InvoiceGenerationCommand(domainEvent.AggregateIdentity.Value,
               domainEvent.AggregateEvent.MerchantTransactionId,
               domainEvent.AggregateEvent.Subscriber,
               domainEvent.AggregateEvent.CardDetails.HolderName);

            this.Publish(command);
            this.Emit(new BillingItemStatusUpdatedSagaEvent("Activated", domainEvent.AggregateEvent.ItemId));

            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, InvoiceGeneratedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            Emit(new PaymentExecutionSagaCompletedEvent());
            return Task.CompletedTask;
        }

        public void Apply(PaymentInitializedSagaEvent @event) { }
        public void Apply(BillingItemStatusUpdatedSagaEvent @event) { }
        public void Apply(PaymentExecutionSagaCompletedEvent @event)
        {
            Complete();
        }



    }
}
