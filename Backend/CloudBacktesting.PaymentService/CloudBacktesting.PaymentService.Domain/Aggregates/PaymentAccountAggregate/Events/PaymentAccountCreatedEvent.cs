using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events
{
    public class PaymentAccountCreatedEvent : AggregateEvent<PaymentAccount, PaymentAccountId>
    {
        public string Client { get; }

        public PaymentAccountCreatedEvent(string client)
        {
            Client = client;
        }
    }
}
