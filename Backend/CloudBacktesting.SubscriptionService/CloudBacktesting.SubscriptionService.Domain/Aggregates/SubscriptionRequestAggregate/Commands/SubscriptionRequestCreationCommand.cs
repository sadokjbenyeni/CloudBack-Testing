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
        public PaymentAction PaymentAction { get; set; }

        public SubscriptionRequestCreationCommand(string subscriptionAccountId, string type, PaymentAction paymentAction) : base(SubscriptionRequestId.New)
        {
            if (string.IsNullOrEmpty(subscriptionAccountId))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(subscriptionAccountId));
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(type));
            }

            if (string.IsNullOrEmpty(paymentAction.PaymentAccountId))
            {
                throw new ArgumentNullException("Cannot be null or empty", nameof(PaymentAction.PaymentAccountId));
            }

            if (string.IsNullOrEmpty(paymentAction.PaymentMethodId))
            {
                throw new ArgumentNullException("Cannot be null or empty", nameof(paymentAction.PaymentMethodId));
            }

            SubscriptionAccountId = subscriptionAccountId;
            Type = type;
            PaymentAction = paymentAction;
        }
    }
}
