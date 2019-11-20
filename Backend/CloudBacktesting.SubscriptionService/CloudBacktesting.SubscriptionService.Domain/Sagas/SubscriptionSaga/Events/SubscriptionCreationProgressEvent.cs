using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionSaga.Events
{
    public class SubscriptionCreationProgressEvent /*: AggregateEvent<SubscriptionCreationSaga, SubscriptionCreationSagaId>*/
    {
        public SubscriptionId SubscriptionId { get; }
        public int Progress { get; }
        public int Elapsed { get; }
        public DateTime UpdatedAt { get; }

        public SubscriptionCreationProgressEvent(
               SubscriptionId subscriptionId,
               int progress,
               int elapsed,
               DateTime updatedAt)
        {
            SubscriptionId = subscriptionId;
            Progress = progress;
            Elapsed = elapsed;
            UpdatedAt = updatedAt;
        }
    }
}
