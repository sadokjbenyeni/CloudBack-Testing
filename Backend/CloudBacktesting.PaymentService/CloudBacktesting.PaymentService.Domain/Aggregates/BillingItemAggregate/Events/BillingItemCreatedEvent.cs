using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class BillingItemCreatedEvent : AggregateEvent<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; }
        public string PaymentMethodId { get; }
        public string Status { get; }
        public string SubscriptionRequestId { get; }
        public string PaymentMethodStatus { get; }
        public DateTime CreateDate { get; }
        public string Type { get; }


        public BillingItemCreatedEvent(string itemId, string paymentMethodId, string subscriptionRequestId, string status, string paymentMethodStatus, DateTime createDate, string type)
        {
            ItemId = itemId;
            PaymentMethodId = paymentMethodId;
            Status = status;
            SubscriptionRequestId = subscriptionRequestId;
            PaymentMethodStatus = paymentMethodStatus;
            CreateDate = createDate;
            Type = type;
        }
    }
}
