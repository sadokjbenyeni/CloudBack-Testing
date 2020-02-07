using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class SubscriptionRequestLinkToBillingItemCommand : Command<BillingItem, BillingItemId, IExecutionResult>
    {
        public string SubscriptionRequestId { get; set; }

        public SubscriptionRequestLinkToBillingItemCommand(string subscriptionRequestId) : base(BillingItemId.New)
        {
            SubscriptionRequestId = subscriptionRequestId ?? throw new ArgumentNullException(nameof(subscriptionRequestId));
        }
    }
}
