using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class PaymentFailedEvent : AggregateEvent<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; }
        public string PaymentMethodId { get; }
        public DateTime FailureDate { get;  }
        public string Message { get; }

        public PaymentFailedEvent(string itemId, string paymentMethodId, string message, DateTime failureDate)
        {
            ItemId = itemId;
            PaymentMethodId = paymentMethodId;
            Message = message;
            FailureDate = failureDate;
        }
    }
}
