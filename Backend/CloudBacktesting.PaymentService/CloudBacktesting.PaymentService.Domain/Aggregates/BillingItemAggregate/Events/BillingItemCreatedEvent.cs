using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class BillingItemCreatedEvent : AggregateEvent<BillingItem, BillingItemId>
    {

        public string PaymentMethodId { get; }

        public BillingItemCreatedEvent(string paymentMethodId)
        {
            PaymentMethodId = paymentMethodId;
        }
    }
}
