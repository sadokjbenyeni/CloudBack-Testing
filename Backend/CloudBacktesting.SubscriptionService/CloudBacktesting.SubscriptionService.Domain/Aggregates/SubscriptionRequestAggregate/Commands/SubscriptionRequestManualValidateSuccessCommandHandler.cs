using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestManualValidateSuccessCommandHandler : CommandHandler<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestManualValidateSuccessCommand>
    {
        public override Task ExecuteAsync(SubscriptionRequest aggregate, SubscriptionRequestManualValidateSuccessCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.ManualValidate();
            return Task.FromResult(executionResult);
        }
    }
}
