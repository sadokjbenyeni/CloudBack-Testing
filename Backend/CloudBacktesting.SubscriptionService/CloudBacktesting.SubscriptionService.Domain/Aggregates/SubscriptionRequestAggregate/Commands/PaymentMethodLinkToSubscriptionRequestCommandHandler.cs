using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class PaymentMethodLinkToSubscriptionRequestCommandHandler : CommandHandler<SubscriptionRequest, SubscriptionRequestId, PaymentMethodLinkToSubscriptionRequestCommand>
    {
        public override Task ExecuteAsync(SubscriptionRequest aggregate, PaymentMethodLinkToSubscriptionRequestCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.LinktoPaymentMethod(command.PaymentMethodId, command.PaymentAccountId));
        }
    }
}
