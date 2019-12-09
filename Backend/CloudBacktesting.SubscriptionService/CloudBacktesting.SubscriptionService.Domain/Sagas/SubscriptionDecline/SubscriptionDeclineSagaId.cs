using EventFlow.Core;
using EventFlow.Sagas;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionDecline.Event
{
    public class SubscriptionDeclineSagaId : Identity<SubscriptionDeclineSagaId>, ISagaId
    {
        public SubscriptionDeclineSagaId(string value) : base(value) { }
    }
}
