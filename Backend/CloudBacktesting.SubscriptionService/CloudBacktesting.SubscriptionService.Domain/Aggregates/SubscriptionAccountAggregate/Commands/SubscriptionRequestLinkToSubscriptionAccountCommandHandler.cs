using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands
{
    public class SubscriptionRequestLinkToSubscriptionAccountCommandHandler : CommandHandler<SubscriptionAccount, SubscriptionAccountId, SubscriptionRequestLinkToSubscriptionAccountCommand>
    {
        public override Task ExecuteAsync(SubscriptionAccount aggregate, SubscriptionRequestLinkToSubscriptionAccountCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.LinkSubscriptionRequest(command.SubscriptionRequestId, command.SubscriptionRequestStatus, command.SubscriptionRequestType));
        }
    }
}
