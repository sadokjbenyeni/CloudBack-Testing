using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class SubscriptionNRequestLinkToBillingCommandHandler : CommandHandler<BillingItem, BillingItemId, IExecutionResult, SubscriptionNPaymentLinkToBillingCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(BillingItem aggregate, SubscriptionNPaymentLinkToBillingCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.LinkSubscriptionNPaymentToBilling(command.AggregateId.Value, command.PaymentMethodStatus));
        }
    }
}
