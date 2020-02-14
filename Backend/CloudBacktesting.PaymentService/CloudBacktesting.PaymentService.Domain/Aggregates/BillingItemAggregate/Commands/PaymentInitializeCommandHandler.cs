using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class PaymentInitializeCommandHandler : CommandHandler<BillingItem, BillingItemId, IExecutionResult, PaymentInitializeCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(BillingItem aggregate, PaymentInitializeCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.InitializePayment(command.Type));

        }
    }
}
