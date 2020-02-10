using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class PaymentMethodSystemValidateCommandHandler : CommandHandler<PaymentMethod, PaymentMethodId, PaymentMethodSystemValidateCommand>
    {
        public override Task ExecuteAsync(PaymentMethod aggregate, PaymentMethodSystemValidateCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.ValidateBySystem(command.AggregateId,command.Client, command.CardNumber, command.CardType, command.Cryptogram, command.ExpirationYear, command.ExpirationMonth);
            return Task.FromResult(executionResult);
        }
    }
}
