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
        public string SubscriptionAccountId { get; set; }

        public SubscriptionRequestCreationCommand(SubscriptionRequestId aggregateId,string subscriptionAccountId, string subscriber, string type, string status, DateTime subscriptionDate) : base(aggregateId)
        {
            if (string.IsNullOrEmpty(subscriptionAccountId))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(subscriptionAccountId));
            }

            if (string.IsNullOrEmpty(subscriber))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(subscriber));
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(type));
            }

            if (string.IsNullOrEmpty(status))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(status));
            }

            SubscriptionAccountId = subscriptionAccountId;
            Subscriber = subscriber;
            Type = type;
            Status = status;
            SubscriptionDate = subscriptionDate;
        }
    }
}
