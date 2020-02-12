using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.BillingFailing.Events
{
    public class BillingFailingSagaCompletedEvent : AggregateEvent<BillingFailingSaga, BillingFailingSagaId>
    {
    }
}
