using EventFlow.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate
{
    public class BillingItemId : Identity<BillingItemId>
    {
        public BillingItemId(string value) : base(value)
        {

        }
    }
}
