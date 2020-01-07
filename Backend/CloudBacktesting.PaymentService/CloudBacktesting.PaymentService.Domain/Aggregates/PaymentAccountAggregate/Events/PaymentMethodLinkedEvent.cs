using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events
{
    public class PaymentMethodLinkedEvent : AggregateEvent<PaymentAccount, PaymentAccountId>, IPaymentSagaMethodId
    {
        public string MethodId { get; }
        public string Client { get; }

        public PaymentMethodLinkedEvent(string methodId, string client)
        {
            MethodId = methodId;
            Client = client;
        }
    }
}
