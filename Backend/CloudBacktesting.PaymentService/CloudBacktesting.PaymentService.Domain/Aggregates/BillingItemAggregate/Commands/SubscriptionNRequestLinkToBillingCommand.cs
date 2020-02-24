using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class SubscriptionNPaymentLinkToBillingCommand : Command<BillingItem, BillingItemId>
    {
        public string SubscriptionRequestId { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentMethodStatus { get; set; }
        public string SubscriptionType { get; set; }

        public SubscriptionNPaymentLinkToBillingCommand(BillingItemId billingItemId, string subscriptionRequestId, string paymentMethodId, string paymentMethodStatus, string subscriptionType) : base(billingItemId)
        {
            SubscriptionRequestId = subscriptionRequestId ?? throw new ArgumentNullException(nameof(subscriptionRequestId));
            PaymentMethodId = paymentMethodId;
            PaymentMethodStatus = paymentMethodStatus;
            SubscriptionType = subscriptionType;
        }
    }
}

