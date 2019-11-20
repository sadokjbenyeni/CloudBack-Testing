using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount;
using System;


namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionSaga.Events
{
    public class SubscriptionCreationEndedEvent /*: AggregateEvent<SubscriptionCreationSaga, SubscriptionCreationSagaId>*/
    {
        public SubscriptionId Id { get; }
        public SubscriptionAccountId SubscriptionAccountId { get; set; }
        public string UserIdentifier { get; set; }
        public double ElapsedTimeToCreation { get; }
        public DateTime CreationDate { get; set; }
        public string ValidatorUserIdentifier { get; set; }
        public string EnvironmentSetupUserIdentifier { get; set; }
        public string TypeOfSubscription { get; set; }
        public string Status { get; set; }
        public int Progress { get; }
        public int Elapsed { get; }
        public DateTime EndedAt { get; }

        public SubscriptionCreationEndedEvent(
            SubscriptionId subscriptionId,
            int progress,
            int elapsed,
            DateTime endedAt)
        {
            Id = subscriptionId;
            Progress = progress;
            Elapsed = elapsed;
            EndedAt = endedAt;
        }
    }
}
