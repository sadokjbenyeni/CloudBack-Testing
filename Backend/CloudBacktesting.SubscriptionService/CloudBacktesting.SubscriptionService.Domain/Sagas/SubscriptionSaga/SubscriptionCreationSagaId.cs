using Akkatecture.Sagas;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionSaga
{
    public class SubscriptionCreationSagaId : SagaId<SubscriptionCreationSagaId>
    {
        public SubscriptionCreationSagaId(string value) : base(value)
        {
        }
    }
}
