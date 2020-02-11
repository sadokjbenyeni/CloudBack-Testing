using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class PaymentMethodCheckStatusCommand : Command<PaymentMethod, PaymentMethodId>
    {
        public string BillingItemId { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentMethodStatus { get; set; }
        public PaymentMethodCheckStatusCommand(PaymentMethodId paymentMethodId, string billingItemId, string paymentMethodStatus) : base(paymentMethodId)
        {
            BillingItemId = billingItemId;
            PaymentMethodStatus = paymentMethodStatus;
        }
    }
}
