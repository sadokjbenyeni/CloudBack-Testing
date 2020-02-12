using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.BillingFailing.Events
{
    public class BillingItemFailedEmailSentEvent : AggregateEvent<BillingFailingSaga, BillingFailingSagaId>, IBillingSagaItemId
    {
        public string ItemId { get; }
        public string PaymentMethodId { get; }
        public string Message { get; }
        public DateTime FailureDate { get; }

        public BillingItemFailedEmailSentEvent(string billingItemId, string paymentMethodId, string message, DateTime failureDate)
        {
            ItemId = billingItemId;
            PaymentMethodId = paymentMethodId;
            Message = message;
            FailureDate = failureDate;
        }
    }
}
