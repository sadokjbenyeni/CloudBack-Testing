using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentCreation.Events
{
    public class PaymentCreationSagaCompletedEvent : AggregateEvent<PaymentCreationSaga, PaymentCreationSagaId>
    {
    }
}
