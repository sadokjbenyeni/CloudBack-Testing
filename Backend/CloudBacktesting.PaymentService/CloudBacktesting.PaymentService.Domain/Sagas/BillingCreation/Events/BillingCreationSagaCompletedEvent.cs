using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.BillingCreation.Events
{
    public class BillingCreationSagaCompletedEvent : AggregateEvent<BillingCreationSaga, BillingCreationSagaId>
    {
    }
}
