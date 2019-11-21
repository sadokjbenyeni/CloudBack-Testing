using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestCreationCommand : Command<SubscriptionRequest, SubscriptionRequestId, IExecutionResult>
    {
        public string Subscriber { get; }
        public string Type { get; }
        public string Status { get; set; }
        public DateTime SubscriptionDate { get; }

        public SubscriptionRequestCreationCommand(SubscriptionRequestId aggregateId, string subscriber, string type, string status, DateTime subscriptionDate) : base(aggregateId)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (status == null) throw new ArgumentNullException(nameof(status));
            Subscriber = subscriber;
            Type = type;
            Status = status;
            SubscriptionDate = subscriptionDate;
        }
    }
}
