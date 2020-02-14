using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class SubscriptionNRequestLinkToBillingCommandHandler : CommandHandler<BillingItem, BillingItemId, IExecutionResult, SubscriptionNRequestLinkToBillingCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(BillingItem aggregate, SubscriptionNRequestLinkToBillingCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.LinkSubscriptionNPaymentToBilling(command.SubscriptionRequestId, command.PaymentMethodId, command.PaymentMethodStatus));
        }
    }
}
