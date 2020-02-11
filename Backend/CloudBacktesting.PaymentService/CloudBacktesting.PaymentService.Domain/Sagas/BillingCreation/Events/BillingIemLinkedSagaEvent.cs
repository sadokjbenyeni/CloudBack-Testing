using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.BillingCreation.Events
{
    public class BillingIemLinkedSagaEvent : AggregateEvent<BillingCreationSaga, BillingCreationSagaId>, IBillingSagaItemId
    {
        public string ItemId { get; }
        public string PaymentMethodId { get; }
        public string PaymentMethodStatus { get; }

        public BillingIemLinkedSagaEvent(string itemId, string paymentMethodId, string paymentMethodStatus)
        {
            ItemId = itemId;
            PaymentMethodId = paymentMethodId;
            PaymentMethodStatus = paymentMethodStatus;
        }
    }
}
