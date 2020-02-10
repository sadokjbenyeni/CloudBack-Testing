using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events
{
    public class PaymentMethodValidatedEvent : AggregateEvent<PaymentMethod, PaymentMethodId>, IPaymentSagaMethodId
    {
        public string MethodId { get; }
        public DateTime CreationDate { get; set; }

        public PaymentMethodValidatedEvent(string methodId, DateTime creationDate)
        {
            MethodId = methodId;
            CreationDate = creationDate;
        }
    }
}
 