using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class BillingItemSystemDeclineCommandHandler : CommandHandler<BillingItem, BillingItemId, BillingItemSystemDeclineCommand>
    {
        public override Task ExecuteAsync(BillingItem aggregate, BillingItemSystemDeclineCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.DeclineBySystem(command.PaymentMethodId);
            return Task.FromResult(executionResult);
        }
    }
}
