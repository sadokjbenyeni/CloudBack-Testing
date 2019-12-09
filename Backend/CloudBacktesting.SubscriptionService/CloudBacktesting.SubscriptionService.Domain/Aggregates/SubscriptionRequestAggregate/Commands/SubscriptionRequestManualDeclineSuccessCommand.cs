using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestManualDeclineSuccessCommand : Command<SubscriptionRequest, SubscriptionRequestId>, ISubscriptionSagaRequestId
    {
        public string DeclineMessage { get; set; }
        public string RequestId { get; set; }

        public SubscriptionRequestManualDeclineSuccessCommand(string aggregateId, string declineMessage) : base(new SubscriptionRequestId(aggregateId)) 
        {
            DeclineMessage = declineMessage;
            RequestId = aggregateId;
        }
    }
}
