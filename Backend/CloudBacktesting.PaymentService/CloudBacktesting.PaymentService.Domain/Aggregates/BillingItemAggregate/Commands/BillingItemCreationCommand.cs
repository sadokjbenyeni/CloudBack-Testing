using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class BillingItemCreationCommand : Command<BillingItem, BillingItemId, IExecutionResult>
    {
        public string PaymentMethodId { get; set; }
        public string SubscriptionRequestId { get; set; }

        public BillingItemCreationCommand(string paymentMethodId, string subscriptionRequestId) : base(BillingItemId.New)
        {
            PaymentMethodId = paymentMethodId ?? throw new ArgumentNullException(nameof(paymentMethodId));
            SubscriptionRequestId = subscriptionRequestId ?? throw new ArgumentNullException(nameof(subscriptionRequestId));
        }
    }
}
