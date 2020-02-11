using EventFlow.Core;
using EventFlow.Sagas;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.BillingCreation
{
    public class BillingCreationSagaId : Identity<BillingCreationSagaId>, ISagaId
    {
        public BillingCreationSagaId(string value) : base(value) { }
    }
}
