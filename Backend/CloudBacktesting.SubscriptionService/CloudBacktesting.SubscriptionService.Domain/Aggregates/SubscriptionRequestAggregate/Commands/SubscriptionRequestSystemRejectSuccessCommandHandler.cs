using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestSystemRejectSuccessCommandHandler : CommandHandler<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestSystemRejectSuccessCommand>
    {
        public override Task ExecuteAsync(SubscriptionRequest aggregate, SubscriptionRequestSystemRejectSuccessCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.RejectBySystem(command.AggregateId, command.Subscriber);
            return Task.FromResult(executionResult);
        }
    }
}
