//using Akkatecture.Aggregates;
//using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount;
//using System;

//namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionAccountSaga.Events
//{
//    public class SubscriptionAccountCreationStartedEvent : AggregateEvent<SubscriptionAccountCreationSaga, SubscriptionAccountCreationSagaId>
//    {
//        public SubscriptionAccountId SubscriptionAccountId { get; }
//        public DateTime StartedAt { get; }

//        public SubscriptionAccountCreationStartedEvent(
//            SubscriptionAccountId subscriptionAccountId,
//            DateTime startedAt)
//        {
//            SubscriptionAccountId = subscriptionAccountId;
//            StartedAt = startedAt;
//        }
//    }
//}
