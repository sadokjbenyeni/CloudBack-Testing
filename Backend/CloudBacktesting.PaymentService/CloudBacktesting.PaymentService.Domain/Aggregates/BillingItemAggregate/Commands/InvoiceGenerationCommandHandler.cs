using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class InvoiceGenerationCommandHandler : CommandHandler<BillingItem, BillingItemId, IExecutionResult, InvoiceGenerationCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(BillingItem aggregate, InvoiceGenerationCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.GenerateInvoice(command.InvoiceId, command.Method, command.Client, command.CardHolder, command.Address, command.Amount, command.InvoiceDate));
        }
    }
}
