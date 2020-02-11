using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class BillingItemCreationCommandHandler : CommandHandler<BillingItem, BillingItemId, IExecutionResult, BillingItemCreationCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(BillingItem aggregate, BillingItemCreationCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.Create(command.PaymentMethodId, command.PaymentMethodStatus));
        }
    }
}
