using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands
{
    public class SubscriptionAccountCreationCommandHandler : CommandHandler<SubscriptionAccount, SubscriptionAccountId, IExecutionResult, SubscriptionAccountCreationCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(SubscriptionAccount aggregate, SubscriptionAccountCreationCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.Create(command.Subscriber));
        }
    }
}
