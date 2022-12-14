using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Sagas.PaymentCreation.Events;
using CloudBacktesting.PaymentService.Domain.Specifications;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
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
                domainEvent.AggregateEvent.ExpirationYear,
                domainEvent.AggregateEvent.ExpirationMonth); ;

            this.Publish(command);

            this.Emit(new PaymentAccountLinkedSagaEvent(domainEvent.AggregateIdentity.Value,
                                                        domainEvent.AggregateEvent.CardNumber,
                                                        domainEvent.AggregateEvent.CardType,
                                                        domainEvent.AggregateEvent.CardHolder,
                                                        domainEvent.AggregateEvent.ExpirationYear,
                                                        domainEvent.AggregateEvent.ExpirationMonth));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<PaymentAccount, PaymentAccountId, PaymentMethodLinkedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var passLuhenSpec = new PassesLuhenTestSpecification();

            var getCardType = new GetCardTypeFromNumber();

            var isNotNull = new IsNotNullCryptogram();

            if (passLuhenSpec.IsSatisfiedBy(domainEvent.AggregateEvent.CardNumber) == false
                || getCardType.GetCardType(domainEvent.AggregateEvent.CardNumber).Value.ToString() != domainEvent.AggregateEvent.CardType 
                || isNotNull.IsSatisfiedBy(domainEvent.AggregateEvent.Cryptogram) == false)
            {
                var command = new PaymentMethodSystemRejectCommand(domainEvent.AggregateEvent.MethodId, domainEvent.AggregateEvent.Client);
                this.Publish(command);
            }
            else
            {

                var command = new PaymentMethodSystemValidateCommand(domainEvent.AggregateEvent.MethodId, domainEvent.AggregateEvent.Client, domainEvent.AggregateEvent.CardNumber, domainEvent.AggregateEvent.CardType, domainEvent.AggregateEvent.ExpirationYear, domainEvent.AggregateEvent.ExpirationMonth, domainEvent.AggregateEvent.Cryptogram);
                this.Publish(command);
                return Task.CompletedTask;
            }
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
