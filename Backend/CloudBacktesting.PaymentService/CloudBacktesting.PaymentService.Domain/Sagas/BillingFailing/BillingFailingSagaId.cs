using EventFlow.Core;
using EventFlow.Sagas;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.BillingFailing
{
    public class BillingFailingSagaId : Identity<BillingFailingSagaId>, ISagaId
    {
        public BillingFailingSagaId(string value) : base(value) { }
    }
}
