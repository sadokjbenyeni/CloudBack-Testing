using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentExecution.Events
{
    public class PaymentExecutionSagaCompletedEvent : AggregateEvent<PaymentExecutionSaga, PaymentExecutionSagaId>
    {
    }
}
