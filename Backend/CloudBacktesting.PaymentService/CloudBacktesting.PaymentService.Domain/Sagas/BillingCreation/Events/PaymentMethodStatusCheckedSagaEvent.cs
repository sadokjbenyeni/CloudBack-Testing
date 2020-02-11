using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.BillingCreation.Events
{
    public class PaymentMethodStatusCheckedSagaEvent: AggregateEvent<BillingCreationSaga, BillingCreationSagaId>, IBillingSagaItemId
    {
        public string MethodId { get; }
        public string ItemId { get; }
        public string PaymentMethodStatus { get; }

        public PaymentMethodStatusCheckedSagaEvent(string paymentMethodId, string itemId, string status)
        {
            MethodId = paymentMethodId;
            ItemId = itemId;
            PaymentMethodStatus = status;
        }
    }
}
