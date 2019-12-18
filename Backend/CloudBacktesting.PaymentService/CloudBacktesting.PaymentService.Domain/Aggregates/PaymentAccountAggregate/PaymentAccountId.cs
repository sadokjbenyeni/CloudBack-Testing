using EventFlow.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate
{
    public class PaymentAccountId : Identity<PaymentAccountId>
    {
        public PaymentAccountId(string value) : base(value)
        {

        }
    }
}
