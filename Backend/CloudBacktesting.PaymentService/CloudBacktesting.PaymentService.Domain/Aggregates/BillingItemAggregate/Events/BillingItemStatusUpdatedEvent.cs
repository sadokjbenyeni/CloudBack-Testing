using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class BillingItemStatusUpdatedEvent : AggregateEvent<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string Status { get; set; }
        public string ItemId { get; set; }

        public BillingItemStatusUpdatedEvent(string status, string itemId)
        {
            Status = status;
            ItemId = itemId;
        }
    }
}
