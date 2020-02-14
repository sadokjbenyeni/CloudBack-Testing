using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class PaymentExecutionInitializedEvent : AggregateEvent<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; set; }
        public string PaymentMethodId { get; set; }
        public string MerchantTransactionId { get; set; }
        public string Type { get; set; }

        public PaymentExecutionInitializedEvent(string paymentMethodId, string merchantTransactionId, string type)
        {
            MerchantTransactionId = merchantTransactionId;
            PaymentMethodId = paymentMethodId;
            Type = type;
        }
    }
}
