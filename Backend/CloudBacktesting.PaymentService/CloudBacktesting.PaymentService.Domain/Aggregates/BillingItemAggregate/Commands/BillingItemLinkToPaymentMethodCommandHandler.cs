using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class BillingItemLinkToPaymentMethodCommandHandler : CommandHandler<BillingItem, BillingItemId, IExecutionResult, BillingItemLinkToPaymentMethodCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(BillingItem aggregate, BillingItemLinkToPaymentMethodCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.LinkBillingToPayment(command.PaymentMethodId));
        }
    }
}
