using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository;
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
        public string PaymentMethodId { get; set; }
        public string PaymentAccountId { get; set; }
        public SubscriptionRequestCreationCommand(string subscriptionAccountId, string type, string paymentMethodId, string paymentAccountId) : base(SubscriptionRequestId.New)
        {
            if (string.IsNullOrEmpty(subscriptionAccountId))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(subscriptionAccountId));
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(type));
            }

            if (string.IsNullOrEmpty(paymentAccountId))
            {
                throw new ArgumentNullException("Cannot be null or empty", nameof(paymentAccountId));
            }

            if (string.IsNullOrEmpty(paymentMethodId))
            {
                throw new ArgumentNullException("Cannot be null or empty", nameof(paymentMethodId));
            }

            SubscriptionAccountId = subscriptionAccountId;
            Type = type;
            PaymentAccountId = paymentAccountId; 
            PaymentMethodId = paymentMethodId;
        }
    }
}
