using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class BillingItemLinkToPaymentMethodCommand : Command<BillingItem, BillingItemId, IExecutionResult>
    {
        public string PaymentMethodId { get; set; }

        public BillingItemLinkToPaymentMethodCommand(string paymentMethodId) : base(BillingItemId.New)
        {
            PaymentMethodId = paymentMethodId ?? throw new ArgumentNullException(nameof(paymentMethodId));
        }
    }
}
