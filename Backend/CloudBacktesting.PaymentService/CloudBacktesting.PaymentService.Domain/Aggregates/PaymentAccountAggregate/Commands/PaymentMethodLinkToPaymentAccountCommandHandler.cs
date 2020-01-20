using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands
{
    public class PaymentMethodLinkToPaymentAccountCommandHandler : CommandHandler<PaymentAccount, PaymentAccountId, PaymentMethodLinkToPaymentAccountCommand>
    {
        public override Task ExecuteAsync(PaymentAccount aggregate, PaymentMethodLinkToPaymentAccountCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.LinkPaymentMethod(command.MethodId, command.PaymentMethodCardNumber, command.PaymentMethodCardType));
        }
    }
}
