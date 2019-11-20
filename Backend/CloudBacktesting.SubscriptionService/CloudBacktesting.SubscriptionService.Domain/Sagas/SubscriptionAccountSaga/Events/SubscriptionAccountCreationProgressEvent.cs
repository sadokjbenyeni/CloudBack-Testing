using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionAccountSaga.Events
{
    public class SubscriptionAccountCreationProgressEvent /*: AggregateEvent<SubscriptionAccountCreationSaga, SubscriptionAccountCreationSagaId>*/
    {
        public SubscriptionAccountId SubscriptionAccountId { get; }
        public int Progress { get; }
        public int Elapsed { get; }
        public DateTime UpdatedAt { get; }

        public SubscriptionAccountCreationProgressEvent(
               SubscriptionAccountId subscriptionAccountId,
               int progress,
               int elapsed,
               DateTime updatedAt)
        {
            SubscriptionAccountId = subscriptionAccountId;
            Progress = progress;
            Elapsed = elapsed;
            UpdatedAt = updatedAt;
        }
    }
}
