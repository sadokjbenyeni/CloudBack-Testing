using EventFlow.Core;
using EventFlow.Sagas;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionCreation
{
    public class SubscriptionCreationSagaId : Identity<SubscriptionCreationSagaId>, ISagaId
    {
        public SubscriptionCreationSagaId(string value) : base(value) { }
    }
}
