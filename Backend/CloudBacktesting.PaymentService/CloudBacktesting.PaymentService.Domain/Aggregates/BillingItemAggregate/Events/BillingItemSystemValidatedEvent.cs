using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class BillingItemSystemValidatedEvent : AggregateEvent<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; }
        public string PaymentMethodId { get; }

        public BillingItemSystemValidatedEvent(string itemId, string paymentMethodId)
        {
            ItemId = itemId;
            PaymentMethodId = paymentMethodId;
        }
    }
}
