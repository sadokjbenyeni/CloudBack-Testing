using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestManualDeclineSuccessCommandHandler : CommandHandler<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualDeclineSuccessCommand>
    {
        public override Task ExecuteAsync(SubscriptionRequest aggregate, SubscriptionRequestManualDeclineSuccessCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.ManualDecline(command.DeclineMessage);
            return Task.FromResult(executionResult);
        }
    }
}
