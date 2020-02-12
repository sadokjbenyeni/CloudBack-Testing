using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class BillingItemLinkToPaymentMethodCommand : Command<BillingItem, BillingItemId>
    {
        public string PaymentMethodId { get; set; }
        public string PaymentMethodStatus { get; set; }

        public BillingItemLinkToPaymentMethodCommand(BillingItemId itemId, string paymentMethodId, string paymentMethodStatus) : base(itemId)
        {
            PaymentMethodId = paymentMethodId ?? throw new ArgumentNullException(nameof(paymentMethodId));
            PaymentMethodId = paymentMethodId;
            PaymentMethodStatus = paymentMethodStatus;
        }
    }
}
