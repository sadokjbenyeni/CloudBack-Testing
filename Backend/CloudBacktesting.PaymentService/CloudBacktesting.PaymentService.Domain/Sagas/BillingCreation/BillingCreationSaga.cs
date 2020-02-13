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
                                       ISagaIsStartedBy<BillingItem, BillingItemId, BillingItemCreatedEvent>,
                                       ISagaHandles<BillingItem, BillingItemId, SubscriptionRequestToBillingItemLinkedEvent>,
                                       ISagaHandles<PaymentMethod, PaymentMethodId, PaymentMethodStatusCheckedEvent>,
                                       ISagaHandles<BillingItem, BillingItemId, BillingItemToPaymentMethodLinkedEvent>
    {
        public BillingCreationSaga(BillingCreationSagaId id) : base(id) { }

        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, BillingItemCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new BillingItemLinkToPaymentMethodCommand(new BillingItemId(domainEvent.AggregateEvent.ItemId), domainEvent.AggregateEvent.PaymentMethodId, domainEvent.AggregateEvent.PaymentMethodStatus);
            this.Publish(command);
            this.Emit(new BillingIemLinkedSagaEvent(domainEvent.AggregateEvent.ItemId, domainEvent.AggregateEvent.PaymentMethodId, domainEvent.AggregateEvent.PaymentMethodStatus));
            return Task.CompletedTask;
        }
        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, SubscriptionRequestToBillingItemLinkedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new SubscriptionRequestLinkToBillingItemCommand(new BillingItemId(domainEvent.AggregateEvent.ItemId), domainEvent.AggregateEvent.SubscriptionRequestId);
            this.Publish(command);
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<PaymentMethod, PaymentMethodId, PaymentMethodStatusCheckedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new PaymentMethodCheckStatusCommand(new PaymentMethodId(domainEvent.AggregateEvent.MethodId), domainEvent.AggregateEvent.ItemId, domainEvent.AggregateEvent.PaymentMethodStatus);
            this.Publish(command);
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, BillingItemToPaymentMethodLinkedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            if (domainEvent.AggregateEvent.PaymentMethodStatus == "Validated")
            {
                var command = new BillingItemSystemValidateCommand(domainEvent.AggregateEvent.ItemId, domainEvent.AggregateEvent.PaymentMethodId);
                this.Publish(command);
            }
            else if (domainEvent.AggregateEvent.PaymentMethodStatus == "Declined")
            {
                var command = new BillingItemSystemDeclineCommand(domainEvent.AggregateEvent.ItemId, domainEvent.AggregateEvent.PaymentMethodId);
                this.Publish(command);
            }
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
