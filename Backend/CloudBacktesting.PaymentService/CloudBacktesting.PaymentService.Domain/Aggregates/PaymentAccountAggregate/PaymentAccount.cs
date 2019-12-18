using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate
{
    public class PaymentAccount : AggregateRoot<PaymentAccount, PaymentAccountId>
    {
        public PaymentAccount(PaymentAccountId aggregateId) : base(aggregateId)
        {

        }

        internal IExecutionResult Create()
        {
            throw new NotImplementedException();
        }
    }
}
