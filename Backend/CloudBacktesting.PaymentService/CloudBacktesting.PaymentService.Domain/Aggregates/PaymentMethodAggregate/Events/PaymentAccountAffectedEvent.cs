using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events
{
    public class PaymentAccountAffectedEvent : AggregateEvent<PaymentMethod, PaymentMethodId>
    {
        public string Client { get; set; }
        public PaymentAccountAffectedEvent(string client)
        {
            Client = client;
        }
    }
}
