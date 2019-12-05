using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestManualDeclineSuccessCommand : Command<SubscriptionRequest, SubscriptionRequestId>
    {
        public string DeclineMessage { get; set; }

        public SubscriptionRequestManualDeclineSuccessCommand(SubscriptionRequestId aggregateId, string declineMessage) : base(aggregateId) 
        {
            DeclineMessage = declineMessage;
        }
    }
}
