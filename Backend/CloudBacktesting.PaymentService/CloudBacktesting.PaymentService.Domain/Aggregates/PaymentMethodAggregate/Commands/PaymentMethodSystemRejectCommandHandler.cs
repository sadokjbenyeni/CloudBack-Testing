using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class PaymentMethodSystemRejectCommandHandler : CommandHandler<PaymentMethod, PaymentMethodId, PaymentMethodSystemRejectCommand>
    {
        public override Task ExecuteAsync(PaymentMethod aggregate, PaymentMethodSystemRejectCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.RejectBySystem(command.AggregateId, command.Client);
            return Task.FromResult(executionResult);
        }
    }
}
