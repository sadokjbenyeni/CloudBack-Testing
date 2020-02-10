using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events
{
    public class BillingItemToPaymentMethodLinkedEvent : AggregateEvent<PaymentMethod, PaymentMethodId>
    {
        public string BillingItemId { get; set; }

        public BillingItemToPaymentMethodLinkedEvent(string billingItemId)
        {
            BillingItemId = billingItemId;

        }
    }
}
