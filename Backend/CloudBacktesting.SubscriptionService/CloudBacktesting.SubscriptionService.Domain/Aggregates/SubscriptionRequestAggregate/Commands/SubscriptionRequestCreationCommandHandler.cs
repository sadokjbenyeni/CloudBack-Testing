using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestCreationCommandHandler : CommandHandler<SubscriptionRequest, SubscriptionRequestId, IExecutionResult, SubscriptionRequestCreationCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(SubscriptionRequest aggregate, SubscriptionRequestCreationCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.Create(command.SubscriptionAccountId, command.Type, command.PaymentMethodId, command.PaymentAccountId));
        }
    }
}

