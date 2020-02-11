using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class BillingItemToPaymentMethodLinkedEvent : AggregateEvent<BillingItem, BillingItemId>
    {
        public string BillingItemId { get; }
        public string PaymentMethodId { get; }
        public string PaymentMethodStatus { get; set; }

        public BillingItemToPaymentMethodLinkedEvent(string billingItemId, string paymentMethodId, string paymentMethodStatus)
        {
            PaymentMethodId = paymentMethodId;
            BillingItemId = billingItemId;
            PaymentMethodStatus = paymentMethodStatus;
        }
    }
}
