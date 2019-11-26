using EventFlow.Core;
using EventFlow.Sagas;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas
{
    public class SubscriptionCreationSagaId : Identity<SubscriptionCreationSagaId>
    {
        public SubscriptionCreationSagaId(string value) : base(value){ }
    }
}
