using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Sagas.PaymentCreation.Events;
using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentCreation
{
    public class PaymentCreationSaga : AggregateSaga<PaymentCreationSaga, PaymentCreationSagaId, PaymentCreationSagaLocator>,
                                       ISagaIsStartedBy<PaymentMethod, PaymentMethodId, PaymentMethodCreatedEvent>,
                                       ISagaHandles<PaymentAccount, PaymentAccountId, PaymentMethodLinkedEvent>,
                                       ISagaHandles<PaymentMethod, PaymentMethodId, PaymentMethodValidatedEvent>
    {
        public PaymentCreationSaga(PaymentCreationSagaId id) : base(id) { }
        
        public Task HandleAsync(IDomainEvent<PaymentMethod, PaymentMethodId, PaymentMethodCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellatonToken)
        {
            var command = new PaymentMethodLinkToPaymentAccountCommand(new PaymentAccountId(domainEvent.AggregateEvent.PaymentAccountId),
                domainEvent.AggregateIdentity.Value,
                domainEvent.AggregateEvent.CardNumber,
                domainEvent.AggregateEvent.CardType,
                domainEvent.AggregateEvent.CardHolder,
                domainEvent.AggregateEvent.ExpirationDate);

            this.Publish(command);

            this.Emit(new PaymentAccountLinkedSagaEvent(domainEvent.AggregateIdentity.Value,
                                                        domainEvent.AggregateEvent.CardNumber,
                                                        domainEvent.AggregateEvent.CardType,
                                                        domainEvent.AggregateEvent.CardHolder,
                                                        domainEvent.AggregateEvent.ExpirationDate));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<PaymentAccount, PaymentAccountId, PaymentMethodLinkedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new PaymentMethodSystemValidateCommand(domainEvent.AggregateEvent.MethodId, domainEvent.AggregateEvent.Client, domainEvent.AggregateEvent.CardNumber, domainEvent.AggregateEvent.CardType, domainEvent.AggregateEvent.Cryptogram);
            this.Publish(command);
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<PaymentMethod, PaymentMethodId, PaymentMethodValidatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            Emit(new PaymentCreationSagaCompletedEvent());
            return Task.CompletedTask;
        }

        public void Apply(PaymentAccountLinkedSagaEvent @event) { }

        public void Apply(PaymentCreationSagaCompletedEvent sagaEvent)
        {
            Complete();
        }
    }
}
