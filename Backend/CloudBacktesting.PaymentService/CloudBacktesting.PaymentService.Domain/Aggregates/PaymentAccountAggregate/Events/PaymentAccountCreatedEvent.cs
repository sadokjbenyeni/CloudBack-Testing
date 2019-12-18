using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events
{
    public class PaymentAccountCreatedEvent : AggregateEvent<PaymentAccount, PaymentAccountId>
    {
        public PaymentAccountCreatedEvent()
        {

        }
    }
}
