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

        public IExecutionResult Create(string paymentAccountId, string cardNumber, string cardType, string cardHolder, int expirationYear, int expirationMonth, string cryptogram)
        {
            var @event = new PaymentMethodCreatedEvent(this.Id.Value, paymentAccountId, cardNumber, cardType, cardHolder, cryptogram, expirationYear, expirationMonth);
            Emit(@event);
            return ExecutionResult.Success();
        }

        public IExecutionResult SystemValidate(PaymentMethodId paymentMethodId, string cardNumber, string cardType, string cryptogram, int expirationYear, int expirationMonth)
        {
            var passLuhenSpec = new PassesLuhenTestSpecification();
            if (passLuhenSpec.IsLuhenValidated(cardNumber) == false)
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
            var @event = new PaymentMethodValidatedEvent(paymentMethodId.ToString());
            Emit(@event);
            return ExecutionResult.Success();
        }

        public void Apply(PaymentMethodCreatedEvent @event)
        {
            this.paymentAccountId = @event.PaymentAccountId;
        }
        public void Apply(PaymentMethodValidatedEvent @event) { }
    }
}
