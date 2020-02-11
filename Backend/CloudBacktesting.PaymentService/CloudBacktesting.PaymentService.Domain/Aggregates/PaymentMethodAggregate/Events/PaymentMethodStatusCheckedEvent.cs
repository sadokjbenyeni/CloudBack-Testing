using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events
{
    public class PaymentMethodStatusCheckedEvent : AggregateEvent<PaymentMethod, PaymentMethodId>, IBillingSagaItemId
    {
        public string MethodId { get; set; }
        public string ItemId { get; }
        public string PaymentMethodStatus { get; }

        public PaymentMethodStatusCheckedEvent(string paymentMethodId, string itemId, string status)
        {
            MethodId = paymentMethodId;
            ItemId = itemId;
            PaymentMethodStatus = status;
        }
    }
}
