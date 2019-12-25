using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas
{
    public interface IPaymentSagaMethodId
    {
        string MethodId { get; }
    }
}
