using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class PaymentExecutionCommandHandler : CommandHandler<BillingItem, BillingItemId, IExecutionResult, PaymentExecutionCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(BillingItem aggregate, PaymentExecutionCommand command, CancellationToken cancellationToken)
        {
            return aggregate.ExecutePayment(command.MerchantTransactionId, command.AggregateId.Value, command.Subscriber, command.CardDetails, command.Currency, command.Amount);
        }
    }
}
