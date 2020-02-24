using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class PaymentInitializeCommand : Command<BillingItem, BillingItemId>
    {
        public string SubscriptionType { get; set; }
        public PaymentInitializeCommand(BillingItemId billingItemId, string subscriptionType) : base(billingItemId)
        {
            SubscriptionType = subscriptionType;
        }
    }
}
