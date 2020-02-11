using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class BillingItemCreatedEvent : AggregateEvent<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; set; }
        public string PaymentMethodId { get; }
        public string Status { get; set; }
        public string PaymentMethodStatus { get; set; }
        public DateTime CreateDate { get; set; }


        public BillingItemCreatedEvent(string itemId, string paymentMethodId, string status, string paymentMethodStatus, DateTime createDate)
        {
            ItemId = itemId;
            PaymentMethodId = paymentMethodId;
            Status = status;
            PaymentMethodStatus = paymentMethodStatus;
            CreateDate = createDate;
        }
    }
}
