using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class PaymentMethodCreationCommandHandler : CommandHandler<PaymentMethod,PaymentMethodId, IExecutionResult, PaymentMethodCreationCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(PaymentMethod aggregate, PaymentMethodCreationCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.Create(command.PaymentAccountId, command.CardNumber, command.CardType, command.Cryptogram, command.ExpirationDate));
        }
    }
}
