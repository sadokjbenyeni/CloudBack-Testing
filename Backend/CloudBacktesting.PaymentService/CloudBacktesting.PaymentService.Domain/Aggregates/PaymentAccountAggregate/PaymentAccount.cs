using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate
{
    public class PaymentAccount : AggregateRoot<PaymentAccount, PaymentAccountId>, IEmit<PaymentAccountCreatedEvent>
    {
        private string client;
        
        public PaymentAccount(PaymentAccountId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string client)
        {
            Emit(new PaymentAccountCreatedEvent(client));
            return ExecutionResult.Success();
        }

        public void Apply(PaymentAccountCreatedEvent aggregateEvent)
        {
            this.client = aggregateEvent.Client;
        }
    }
}
