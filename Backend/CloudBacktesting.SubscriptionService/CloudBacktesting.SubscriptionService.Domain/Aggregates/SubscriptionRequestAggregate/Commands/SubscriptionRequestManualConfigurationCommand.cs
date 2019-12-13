using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestManualConfigurationCommand : Command<SubscriptionRequest, SubscriptionRequestId>
    {
        public SubscriptionRequestManualConfigurationCommand(SubscriptionRequestId aggregateId) : base(aggregateId)
        {
        }
    }
}
