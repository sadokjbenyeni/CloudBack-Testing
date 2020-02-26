using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentExecution.Events
{
    public class PaymentExecutedSagaEvent : AggregateEvent<PaymentExecutionSaga, PaymentExecutionSagaId>, IBillingSagaItemId
    {
        public string ItemId { get; }
        public string MerchantTransactionId { get; }


        public PaymentExecutedSagaEvent(string itemId, string merchantTransactionId)
        {
            ItemId = itemId;
            MerchantTransactionId = merchantTransactionId;
        }
    }
}
