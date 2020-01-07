using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestManualConfigurationCommandHandler : CommandHandler<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualConfigurationCommand>
    {
        public override Task ExecuteAsync(SubscriptionRequest aggregate, SubscriptionRequestManualConfigurationCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.ManualConfigure(command.AggregateId, command.Message);
            return Task.FromResult(executionResult);
        }
    }
}
