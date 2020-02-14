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

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class PaymentExecutionCommandHandler : CommandHandler<PaymentMethod, PaymentMethodId, PaymentExecutionCommand>
    {
        public override Task ExecuteAsync(PaymentMethod aggregate, PaymentExecutionCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.ExecutePayment(command.ItemId, command.MerchantTransactionId, command.SubsriptionRequestId, command.CardDetails, command.Type, command.Subscriber);
            return Task.FromResult(executionResult);
        }
    }
}
