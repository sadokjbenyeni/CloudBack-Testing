using EventFlow.Aggregates;
using EventFlow.Sagas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentExecution
{
    public class PaymentExecutionSagaLocator : ISagaLocator
    {
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            if (!(domainEvent.GetAggregateEvent() is IBillingSagaItemId itemId))
            {
                throw new ArgumentNullException("Cannot build a saga identifier");
            }
            return Task.FromResult<ISagaId>(new PaymentExecutionSagaId($"paymentexecutionsaga-{itemId.ItemId.Remove(0, 12)}"));
        }
    }
}
