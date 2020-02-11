using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class BillingItemSystemDeclinedEvent : AggregateEvent<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; }
        public string PaymentMethodId { get; }

        public BillingItemSystemDeclinedEvent(string itemId, string paymentMethodId)
        {
            ItemId = itemId;
            PaymentMethodId = paymentMethodId;
        }
    }
}
