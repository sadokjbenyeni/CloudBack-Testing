using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Specifications;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate
{
    public class PaymentMethod : AggregateRoot<PaymentMethod, PaymentMethodId>, IEmit<PaymentMethodCreatedEvent>

    {
        public string paymentAccountId;

        public PaymentMethod(PaymentMethodId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string paymentAccountId, string cardNumber, string cardType, string cardHolder, string expirationYear, string expirationMonth, string cryptogram)
        {
            var @event = new PaymentMethodCreatedEvent(this.Id.Value, paymentAccountId, "Creating", cardNumber, cardType, cardHolder, cryptogram, expirationYear, expirationMonth);
            Emit(@event);
            return ExecutionResult.Success();
        }

        public IExecutionResult ValidateBySystem(PaymentMethodId paymentMethodId,string client, string cardNumber, string cardType, string cryptogram, string expirationYear, string expirationMonth)
        {
            var passLuhenSpec = new PassesLuhenTestSpecification();
            if (passLuhenSpec.IsSatisfiedBy(cardNumber) == false)
            {
                return ExecutionResult.Failed(passLuhenSpec.WhyIsNotSatisfiedBy("Card didn't pass Luhen algorithm"));
            }
            var getCardType = new GetCardTypeFromNumber();
            if (getCardType.GetCardType(cardNumber).Value.ToString() != cardType)
            {
                return ExecutionResult.Failed("Card type provided is not correct");
            }
            var isNotNull = new IsNotNullCryptogram();
            if (isNotNull.IsSatisfiedBy(cryptogram) == false)
            {
                return ExecutionResult.Failed(isNotNull.WhyIsNotSatisfiedBy("Cryptogram can't be null"));
            }
            Emit(new PaymentAccountAffectedEvent(client));
            Emit(new PaymentMethodStatusUpdatedEvent("Validated", paymentAccountId.ToString()));
            Emit(new PaymentMethodValidatedEvent(this.Id.Value, DateTime.UtcNow));
            return ExecutionResult.Success();
        }
        public IExecutionResult RejectBySystem(PaymentMethodId paymentMethod, string client)
        {
            Emit(new PaymentMethodStatusUpdatedEvent("Declined", paymentMethod.ToString()));
            Emit(new PaymentMethodDeclinedEvent(this.Id.Value, this.paymentAccountId, client, "You have been rejected by the system, Please check your card information", DateTime.UtcNow));
            return ExecutionResult.Success();
        }

        public void Apply(PaymentMethodCreatedEvent @event)
        {
            this.paymentAccountId = @event.PaymentAccountId;
        }
        public void Apply(PaymentMethodValidatedEvent @event) { }
        public void Apply(PaymentAccountAffectedEvent @event) { }


    }
}
