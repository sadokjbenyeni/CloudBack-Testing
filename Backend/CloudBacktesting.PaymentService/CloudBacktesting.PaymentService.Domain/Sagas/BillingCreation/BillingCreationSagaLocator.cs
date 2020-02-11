using EventFlow.Aggregates;
using EventFlow.Sagas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Sagas.BillingCreation
{
    public class BillingCreationSagaLocator : ISagaLocator
    {
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            if(!(domainEvent.GetAggregateEvent() is IBillingSagaItemId itemId))
            {
                throw new ArgumentException("Cannot build the saga Identifier");
            }
            return Task.FromResult<ISagaId>(new BillingCreationSagaId($"billingcreationsaga-{itemId.ItemId.Remove(0, 12)}"));
        }
    }
}
