using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands
{
    public class PaymentAccountCreationCommandHandler : CommandHandler<PaymentAccount, PaymentAccountId, IExecutionResult, PaymentAccountCreationCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(PaymentAccount aggregate, PaymentAccountCreationCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(aggregate.Create());
        }
    }
}
