using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionSaga.Events
{
    public class SubscriptionCreationStartedEvent : AggregateEvent<SubscriptionCreationSaga, SubscriptionCreationSagaId>
    {
        public SubscriptionId SubscriptionId { get; }
        public DateTime StartedAt { get; }

        public SubscriptionCreationStartedEvent(
            SubscriptionId subscriptionId,
            DateTime startedAt)
        {
            SubscriptionId = subscriptionId;
            StartedAt = startedAt;
        }
    }
}
