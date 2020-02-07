using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class SubscriptionRequestLinkToBillingItemCommandHandler : CommandHandler<BillingItem, BillingItemId, IExecutionResult, SubscriptionRequestLinkToBillingItemCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(BillingItem aggregate, SubscriptionRequestLinkToBillingItemCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.LinkSubscriptionToBilling(command.SubscriptionRequestId));
        }
    }
}
