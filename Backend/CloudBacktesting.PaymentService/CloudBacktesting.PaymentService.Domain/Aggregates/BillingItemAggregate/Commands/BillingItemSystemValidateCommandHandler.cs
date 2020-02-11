using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class BillingItemSystemValidateCommandHandler : CommandHandler<BillingItem, BillingItemId, BillingItemSystemValidateCommand>
    {
        public override Task ExecuteAsync(BillingItem aggregate, BillingItemSystemValidateCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.ValidateBySystem(command.AggregateId, command.PaymentMethodId);
            return Task.FromResult(executionResult);
        }
    }
}
