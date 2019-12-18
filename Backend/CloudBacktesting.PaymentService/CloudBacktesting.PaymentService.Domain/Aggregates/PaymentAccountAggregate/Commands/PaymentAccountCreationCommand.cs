using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands
{
    public class PaymentAccountCreationCommand : Command<PaymentAccount, PaymentAccountId, IExecutionResult>
    {
        public PaymentAccountCreationCommand() : base(PaymentAccountId.New)
        {

        }
    }
}
