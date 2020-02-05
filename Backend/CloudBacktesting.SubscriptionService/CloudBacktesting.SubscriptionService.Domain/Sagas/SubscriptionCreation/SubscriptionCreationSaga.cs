using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Events;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionCreation.Events;
using EventFlow.Aggregates;
using EventFlow.Exceptions;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionCreation
{
    public class SubscriptionCreationSaga : AggregateSaga<SubscriptionCreationSaga, SubscriptionCreationSagaId, SubscriptionCreationSagaLocator>,
                                            ISagaIsStartedBy<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent>,
                                            ISagaHandles<SubscriptionAccount, SubscriptionAccountId, SubscriptionRequestLinkedEvent>,
                                            ISagaHandles<SubscriptionRequest, SubscriptionRequestId, PaymentMethodLinkedEvent>,
                                            ISagaHandles<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestValidatedEvent>


    {
        public SubscriptionCreationSaga(SubscriptionCreationSagaId id) : base(id)
        {
        }

        public  Task HandleAsync(IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new SubscriptionRequestLinkToSubscriptionAccountCommand(new SubscriptionAccountId(domainEvent.AggregateEvent.SubscriptionAccountId), 
                domainEvent.AggregateIdentity.Value, 
                domainEvent.AggregateEvent.Status, 
                domainEvent.AggregateEvent.Type,
                domainEvent.AggregateEvent.PaymentMethodId,
                domainEvent.AggregateEvent.PaymentAccountId);
            this.Publish(command);

            this.Emit(new SubscriptionAccountLinkedSagaEvent(domainEvent.AggregateIdentity.Value, domainEvent.AggregateEvent.Status, domainEvent.AggregateEvent.Type));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<SubscriptionAccount, SubscriptionAccountId, SubscriptionRequestLinkedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            if(domainEvent.AggregateEvent.SubscriptionRequestStatus == "Active")
            {
                var command = new SubscriptionRequestSystemRejectSuccessCommand(domainEvent.AggregateEvent.RequestId, domainEvent.AggregateEvent.Subscriber, domainEvent.AggregateEvent.OrderId);
                this.Publish(command);
            }
            else
            {
                var command = new SubscriptionRequestSystemValidateSuccessCommand(new SubscriptionRequestId(domainEvent.AggregateEvent.RequestId), domainEvent.AggregateEvent.Subscriber, domainEvent.AggregateEvent.OrderId);
                this.Publish(command);
            }
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<SubscriptionRequest, SubscriptionRequestId, PaymentMethodLinkedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new PaymentMethodLinkToSubscriptionRequestCommand(domainEvent.AggregateIdentity, domainEvent.AggregateEvent.PaymentMethodId, domainEvent.AggregateEvent.PaymentAccountId);
            this.Publish(command);

            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestValidatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            Emit(new SubscriptionCreationSagaCompletedEvent());
            return Task.CompletedTask;
        }


        public void Apply(SubscriptionAccountLinkedSagaEvent @event) { }

        public void Apply(SubscriptionCreationSagaCompletedEvent sagaEvent)
        {
            Complete();
        }
    }
}
