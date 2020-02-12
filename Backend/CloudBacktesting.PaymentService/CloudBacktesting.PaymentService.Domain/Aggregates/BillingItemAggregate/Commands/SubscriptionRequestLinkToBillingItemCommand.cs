using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class SubscriptionRequestLinkToBillingItemCommand : Command<BillingItem, BillingItemId>
    {
        public string SubscriptionRequestId { get; set; }

        public SubscriptionRequestLinkToBillingItemCommand(string billingItemId, string subscriptionRequestId) : base(new BillingItemId(billingItemId))
        {
            SubscriptionRequestId = subscriptionRequestId ?? throw new ArgumentNullException(nameof(subscriptionRequestId));
        }
    }
}

