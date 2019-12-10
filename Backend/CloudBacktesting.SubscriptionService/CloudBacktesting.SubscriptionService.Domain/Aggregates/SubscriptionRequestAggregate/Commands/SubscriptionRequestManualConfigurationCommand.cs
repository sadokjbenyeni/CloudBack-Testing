using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestManualConfigurationCommand : Command<SubscriptionRequest, SubscriptionRequestId>
    {
        public string Subscriber { get; set; }
        public SubscriptionRequestManualConfigurationCommand(SubscriptionRequestId aggregateId, string subscriber) : base(aggregateId)
        {
            Subscriber = subscriber;
        }
    }
}
