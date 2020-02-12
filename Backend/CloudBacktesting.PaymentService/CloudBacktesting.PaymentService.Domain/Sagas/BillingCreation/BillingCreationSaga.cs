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
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Sagas.BillingCreation
{
    public class BillingCreationSaga : AggregateSaga<BillingCreationSaga, BillingCreationSagaId, BillingCreationSagaLocator>,
                                       ISagaIsStartedBy<PaymentMethod, PaymentMethodId, PaymentMethodStatusCheckedEvent>,
                                       ISagaHandles<BillingItem, BillingItemId, BillingItemCreatedEvent>,
                                       ISagaHandles<BillingItem, BillingItemId, BillingItemToPaymentMethodLinkedEvent>,
                                       ISagaHandles<BillingItem, BillingItemId, PaymentExecutedEvent>,
                                       ISagaHandles<BillingItem, BillingItemId, InvoiceGeneratedEvent>
    {
        public BillingCreationSaga(BillingCreationSagaId id) : base(id) { }


        public Task HandleAsync(IDomainEvent<PaymentMethod, PaymentMethodId, PaymentMethodStatusCheckedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new PaymentMethodCheckStatusCommand(new PaymentMethodId(domainEvent.AggregateEvent.MethodId), domainEvent.AggregateEvent.ItemId, domainEvent.AggregateEvent.PaymentMethodStatus);
            this.Publish(command);
            this.Emit(new PaymentMethodStatusCheckedSagaEvent(domainEvent.AggregateEvent.MethodId, domainEvent.AggregateEvent.ItemId, domainEvent.AggregateEvent.PaymentMethodStatus));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, BillingItemCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new BillingItemLinkToPaymentMethodCommand(new BillingItemId(domainEvent.AggregateEvent.ItemId), domainEvent.AggregateEvent.PaymentMethodId, domainEvent.AggregateEvent.PaymentMethodStatus);
            this.Publish(command);
            this.Emit(new BillingIemLinkedSagaEvent(domainEvent.AggregateEvent.ItemId, domainEvent.AggregateEvent.PaymentMethodId, domainEvent.AggregateEvent.PaymentMethodStatus));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, BillingItemToPaymentMethodLinkedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            if (domainEvent.AggregateEvent.PaymentMethodStatus == "Validated")
            {
                var command = new BillingItemSystemValidateCommand(domainEvent.AggregateEvent.BillingItemId, domainEvent.AggregateEvent.PaymentMethodId);
                this.Publish(command);
            }
            else if (domainEvent.AggregateEvent.PaymentMethodStatus == "Declined")
            {
                var command = new BillingItemSystemDeclineCommand(domainEvent.AggregateEvent.BillingItemId, domainEvent.AggregateEvent.PaymentMethodId);
                this.Publish(command);
            }
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, PaymentExecutedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new PaymentExecutionCommand(domainEvent.AggregateEvent.MerchantTransactionId, domainEvent.AggregateEvent.Subscriber, domainEvent.AggregateEvent.CardDetails, domainEvent.AggregateEvent.Amount, domainEvent.AggregateEvent.Currency);
            this.Publish(command);
            if (domainEvent.AggregateEvent.IsPaymentSuccessful)
            {
                var command = new PaymentExecutionFailureCommand(domainEvent.AggregateIdentity.Value, domainEvent.AggregateEvent.PaymentMethodId);

            }
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, InvoiceGeneratedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            {
                var command = new InvoiceGenerationCommand(domainEvent.AggregateEvent.InvoiceId,
                                           domainEvent.AggregateEvent.Method,
                                           domainEvent.AggregateEvent.Client,
                                           domainEvent.AggregateEvent.CardHolder,
                                           domainEvent.AggregateEvent.Address,
                                           domainEvent.AggregateEvent.Amount,
                                           domainEvent.AggregateEvent.InvoiceDate);
                this.Publish(command);
                Emit(new BillingCreationSagaCompletedEvent());
            }

            return Task.CompletedTask;
        }

        public void Apply(PaymentMethodStatusCheckedSagaEvent @event)
        {

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
