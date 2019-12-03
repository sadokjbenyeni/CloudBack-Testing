using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestAdminValidateSuccessCommandHandler : CommandHandler<SubscriptionRequest, SubscriptionRequestId, SubscriptionRequestAdminValidateSuccessCommand>
    {
        public override Task ExecuteAsync(SubscriptionRequest aggregate, SubscriptionRequestAdminValidateSuccessCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.ValidateByAdmin();
            return Task.FromResult(executionResult);
        }
    }
}
