using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events
{
    public class PaymentMethodDeclinedEvent : AggregateEvent<PaymentMethod, PaymentMethodId>, IPaymentSagaMethodId
    {
        public string MethodId { get; set; }
        public string PaymentAccountId { get; set; }
        public string Client { get; set; }
        public string Message { get; set; }
        public DateTime SystemDeclinedDate { get; set; }

        public PaymentMethodDeclinedEvent(string methodId, string paymentaccountId, string client, string message, DateTime systemDeclinedDate)
        {
            MethodId = methodId;
            PaymentAccountId = paymentaccountId;
            Client = client;
            Message = message;
            SystemDeclinedDate = systemDeclinedDate;
        }
    }
}
