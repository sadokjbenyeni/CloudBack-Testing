using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class BillingItemLinkToPaymentMethodCommandHandler : CommandHandler<PaymentMethod, PaymentMethodId, BillingItemLinkToPaymentMethodCommand>
    {
        public override Task ExecuteAsync(PaymentMethod aggregate, BillingItemLinkToPaymentMethodCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.LinkBillingItem(command.ItemId, command.Type));
        }
    }
}
