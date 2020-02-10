using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class BillingItemLinkToPaymentMethodCommand : Command<PaymentMethod, PaymentMethodId>
    {
        public string BillingItemId { get; set; }

        public BillingItemLinkToPaymentMethodCommand(PaymentMethodId paymentMethodId, string billingItemId) : base(paymentMethodId)
        {
            BillingItemId = billingItemId ?? throw new ArgumentNullException(nameof(paymentMethodId));
        }
    }
}
