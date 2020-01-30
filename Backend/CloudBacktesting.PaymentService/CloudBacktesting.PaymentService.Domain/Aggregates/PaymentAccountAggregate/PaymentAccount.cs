using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate
{
    public class PaymentAccount : AggregateRoot<PaymentAccount, PaymentAccountId>, IEmit<PaymentAccountCreatedEvent>, IEmit<PaymentMethodLinkedEvent>
    {
        private string client;
        private string cardNumber;
        private string cardType;
        
        public PaymentAccount(PaymentAccountId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string client)
        {
            Emit(new PaymentAccountCreatedEvent(client));
            return ExecutionResult.Success();
        }

        public IExecutionResult LinkPaymentMethod(string paymentMethodId, string cardNumber, string cardType, string cryptogram, string expirationYear, string expirationMonth)
        {
            Emit(new PaymentMethodLinkedEvent(paymentMethodId, this.client, cardNumber, cardType, cryptogram, expirationYear, expirationMonth));
            return ExecutionResult.Success();
        }

        public void Apply(PaymentAccountCreatedEvent aggregateEvent)
        {
            this.client = aggregateEvent.Client;
        }

        public void Apply(PaymentMethodLinkedEvent aggregateEvent)
        {
            this.cardNumber = aggregateEvent.CardNumber;
            this.cardType = aggregateEvent.CardType;
        }
    }
}
