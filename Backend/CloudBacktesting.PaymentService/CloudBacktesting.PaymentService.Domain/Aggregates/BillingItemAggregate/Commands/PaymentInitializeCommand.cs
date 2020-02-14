using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class PaymentInitializeCommand : Command<BillingItem, BillingItemId>
    {
        public string Type { get; set; }
        public string MerchantTransactionId { get; set; }
        public PaymentInitializeCommand(BillingItemId billingItemId, string merchatnTransactionId) : base(billingItemId)
        {
            MerchantTransactionId = merchatnTransactionId;
        }
    }
}
