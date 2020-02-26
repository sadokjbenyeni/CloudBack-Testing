using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class PaymentExecutionFailureCommandHandler : CommandHandler<BillingItem, BillingItemId, PaymentExecutionFailureCommand>
    {
        public override Task ExecuteAsync(BillingItem aggregate, PaymentExecutionFailureCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.PaymentFailure(command.AggregateId.Value, command.PaymentMethodId);
            return Task.FromResult(executionResult);
        }
    }
}
