using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestCreationCommand : Command<SubscriptionRequest, SubscriptionRequestId, IExecutionResult>
    {
        public string Type { get; set; }
        public string SubscriptionAccountId { get; set; }

        public SubscriptionRequestCreationCommand(string subscriptionAccountId, string type) : base(SubscriptionRequestId.New)
        {
            if (subscriptionAccountId == null)
            {
                throw new ArgumentException("Cannot be null or empty", nameof(subscriptionAccountId));
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(type));
            }

            SubscriptionAccountId = subscriptionAccountId;
            Type = type;
        }
    }
}
