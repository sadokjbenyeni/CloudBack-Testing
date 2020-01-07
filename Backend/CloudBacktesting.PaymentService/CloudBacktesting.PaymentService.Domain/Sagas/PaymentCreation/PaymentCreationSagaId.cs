using EventFlow.Core;
using EventFlow.Sagas;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentCreation
{
    public class PaymentCreationSagaId : Identity<PaymentCreationSagaId>, ISagaId
    {
        public PaymentCreationSagaId(string value) : base(value) { }
    }
}
