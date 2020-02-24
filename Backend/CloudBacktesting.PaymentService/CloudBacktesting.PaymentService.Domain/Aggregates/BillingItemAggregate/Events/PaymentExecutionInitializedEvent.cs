using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class PaymentExecutionInitializedEvent : AggregateEvent<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; }
        public string PaymentMethodId { get; }
        public string MerchantTransactionId { get; }
        public string Type { get; }

        public PaymentExecutionInitializedEvent(string itemId ,string paymentMethodId, string merchantTransactionId, string type)
        {
            ItemId = itemId;
            MerchantTransactionId = merchantTransactionId;
            PaymentMethodId = paymentMethodId;
            Type = type;
        }
    }
}
