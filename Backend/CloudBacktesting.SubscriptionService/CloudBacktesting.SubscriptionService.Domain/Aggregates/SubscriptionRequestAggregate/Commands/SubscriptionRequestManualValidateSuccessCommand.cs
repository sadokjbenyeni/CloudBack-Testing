using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestManualValidateSuccessCommand : Command<SubscriptionRequest, SubscriptionRequestId>
    {
        public SubscriptionRequestManualValidateSuccessCommand(SubscriptionRequestId aggregateId) : base(aggregateId) {}
    }
}
