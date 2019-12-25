using EventFlow.Aggregates;
using EventFlow.Sagas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentCreation
{
    public class PaymentCreationSagaLocator : ISagaLocator
    {
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            if (!(domainEvent.GetAggregateEvent() is IPaymentSagaMethodId methodId))
            {
                throw new ArgumentException("Cannot build thr saga identifier");
            }
            return Task.FromResult<ISagaId>(new PaymentCreationSagaId($"paymentcreationsaga-{methodId.MethodId.Remove(0, 14)}"));
        }
    }
}