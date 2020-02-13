using EventFlow.Core;
using EventFlow.Sagas;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentExecution
{
    public class PaymentExecutionSagaId : Identity<PaymentExecutionSagaId>, ISagaId
    {
        public PaymentExecutionSagaId(string value) : base(value) { }
    }
}
