using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands;
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
    public class PaymentExecutionCommandHandler : CommandHandler<BillingItem, BillingItemId, PaymentExecutionCommand>
    {
        public override Task ExecuteAsync(BillingItem aggregate, PaymentExecutionCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.ExecutePayment(command.AggregateId.Value ,command.MethodId, command.MerchantTransactionId, command.Subscriber, command.Type, command.CardDetails);
            return Task.FromResult(executionResult);
        }
    }
}
