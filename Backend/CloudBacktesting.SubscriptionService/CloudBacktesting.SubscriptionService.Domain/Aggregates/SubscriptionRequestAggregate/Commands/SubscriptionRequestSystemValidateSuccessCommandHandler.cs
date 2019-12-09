using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestSystemValidateSuccessCommandHandler : CommandHandler<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestSystemValidateSuccessCommand>
    {
        public override Task ExecuteAsync(SubscriptionRequest aggregate, SubscriptionRequestSystemValidateSuccessCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.ValidateBySystem(command.AggregateId ,command.Subscriber);
            return Task.FromResult(executionResult);
        }
    }
}
