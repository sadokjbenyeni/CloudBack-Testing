using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class PaymentMethodCheckStatusCommandHandler : CommandHandler<PaymentMethod, PaymentMethodId, PaymentMethodCheckStatusCommand>
    {
        public override Task ExecuteAsync(PaymentMethod aggregate, PaymentMethodCheckStatusCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.CheckStatus(command.AggregateId, command.BillingItemId, command.PaymentMethodStatus);
            return Task.FromResult(executionResult);
        }
    }
}
