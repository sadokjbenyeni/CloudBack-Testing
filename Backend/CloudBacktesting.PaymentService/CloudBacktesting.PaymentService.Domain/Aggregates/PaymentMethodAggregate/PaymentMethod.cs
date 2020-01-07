using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events;
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

        public IExecutionResult Create(string paymentAccountId, string cardNumber, string cardType, string cryptogram, DateTime expirationDate)
        {
            var @event = new PaymentMethodCreatedEvent(this.Id.Value, paymentAccountId, cardNumber, cardType, cryptogram, expirationDate);
            Emit(@event);
            return ExecutionResult.Success();
        }

        public IExecutionResult SystemValidate(PaymentMethodId paymentMethodId)
        {
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
