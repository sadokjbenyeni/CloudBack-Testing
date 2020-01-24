using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class BillingItemLinkedEvent : AggregateEvent<BillingItem, BillingItemId>
    {
        public string PaymentMethodId { get; set; }

        public BillingItemLinkedEvent(string paymentMethodId)
        {
            PaymentMethodId = paymentMethodId;

        }
    }
}
