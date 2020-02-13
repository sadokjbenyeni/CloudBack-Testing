using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentExecution.Events
{
    //public class PaymentExecutedSagaEvent : AggregateEvent<PaymentExecutionSaga, PaymentExecutionSagaId>, IBillingSagaItemId
    //{
    //    public string ItemId { get; }
    //    public string MerchantTransactionId { get; }
    //    public string PaymentMethodId { get; }
    //    public string Subscriber { get; }
    //    public bool IsPaymentSuccessful { get; }

    //    public PaymentExecutedSagaEvent(string itemId, string merchantTransactionId, string paymentMethodId, string subscriber, bool isPaymentSuccessful)
    //    {
    //        ItemId = itemId;
    //        MerchantTransactionId = merchantTransactionId;
    //        PaymentMethodId = paymentMethodId;
    //        Subscriber = subscriber;
    //        IsPaymentSuccessful = isPaymentSuccessful;
    //    }
    //}
}
