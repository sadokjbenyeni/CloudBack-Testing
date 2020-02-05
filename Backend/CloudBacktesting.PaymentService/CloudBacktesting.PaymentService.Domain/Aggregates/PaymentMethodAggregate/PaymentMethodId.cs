using EventFlow.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate
{
    public class PaymentMethodId : Identity<PaymentMethodId>
    {
        public PaymentMethodId(string value) : base(value)
        {

        }
    }
}
