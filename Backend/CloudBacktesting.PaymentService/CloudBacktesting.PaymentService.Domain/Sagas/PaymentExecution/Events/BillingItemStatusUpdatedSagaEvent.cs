using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentExecution.Events
{
    public class BillingItemStatusUpdatedSagaEvent : AggregateEvent<PaymentExecutionSaga, PaymentExecutionSagaId>, IBillingSagaItemId
    {
        public string Status { get; set; }
        public string ItemId { get; set; }

        public BillingItemStatusUpdatedSagaEvent(string status, string itemId)
        {
            Status = status;
            ItemId = itemId;
        }
    }
}
