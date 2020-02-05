using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestManualConfigurationCommand : Command<SubscriptionRequest, SubscriptionRequestId>
    {
        public string Message { get; set; }
        public SubscriptionRequestManualConfigurationCommand(SubscriptionRequestId aggregateId, string message) : base(aggregateId)
        {
            Message = message;
        }
    }
}
