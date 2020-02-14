using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class PaymentMethodLinkToBillingItemCommandHandler : CommandHandler<PaymentMethod, PaymentMethodId, PaymentMethodLinkToBillingItemCommand>
    {
        public override Task ExecuteAsync(PaymentMethod aggregate, PaymentMethodLinkToBillingItemCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.LinkBillingItem(command.ItemId, command.MerchantTransactionId, command.Type));
        }
    }
}
