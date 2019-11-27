using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestSystemValidateSuccessCommand : Command<SubscriptionRequest, SubscriptionRequestId>
    {
        public SubscriptionRequestSystemValidateSuccessCommand(SubscriptionRequestId aggregateId) : base(aggregateId)
        {
        }
    }
}
