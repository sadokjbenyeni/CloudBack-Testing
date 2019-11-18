using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionAccountSaga.Events
{
    public class SubscriptionAccountCreationEndedEvent : AggregateEvent<SubscriptionAccountCreationSaga, SubscriptionAccountCreationSagaId>
    {
        public SubscriptionAccountId Id { get; }
        public string UserIdentifier { get; set; }
        public int Progress { get; }
        public int Elapsed { get; }
        public DateTime EndedAt { get; }

        public SubscriptionAccountCreationEndedEvent(
            SubscriptionAccountId id,
            string userIdentifier,
            int progress,
            int elapsed,
            DateTime endedAt)
        {
            Id = id;
            UserIdentifier = userIdentifier;
            Progress = progress;
            Elapsed = elapsed;
            EndedAt = endedAt;
        }
    }
}
