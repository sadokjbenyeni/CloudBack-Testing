//using Akkatecture.Aggregates;
//using Akkatecture.Sagas;
//using Akkatecture.Sagas.AggregateSaga;
//using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount;
//using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount.Events;
//using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionAccountSaga.Events;
//using System;
//using System.Threading.Tasks;

//namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionAccountSaga
//{
//    public class SubscriptionAccountCreationSaga : AggregateSaga<SubscriptionAccountCreationSaga, SubscriptionAccountCreationSagaId, SubscriptionAccountCreationSagaState>,
//       ISagaIsStartedByAsync<SubscriptionAccount, SubscriptionAccountId, SubscriptionAccountCreatedEvent>
//    {
//        public async Task HandleAsync(IDomainEvent<SubscriptionAccount, SubscriptionAccountId, SubscriptionAccountCreatedEvent> domainEvent)
//        {
//            //simulates a long running process
//            var subscriptionAccountId = domainEvent.AggregateIdentity;
//            var userIdentifier = domainEvent.AggregateEvent.SubscriptionUser;
//            var startedEvent = new SubscriptionAccountCreationStartedEvent(subscriptionAccountId, DateTime.UtcNow);
//            var started = DateTimeOffset.UtcNow;
//            Emit(startedEvent);

//            var rng = new Random();
//            var progress = 0;

//            while (progress < 100)
//            {
//                var delay = rng.Next(1, 3);

//                await Task.Delay(delay * 1000);
//                progress += rng.Next(5, 15);
//                var elapsed = DateTimeOffset.UtcNow - started;
//                var progressEvent = new SubscriptionAccountCreationProgressEvent(subscriptionAccountId, progress, (int)elapsed.TotalSeconds, DateTime.UtcNow);
//                Emit(progressEvent);
//            }

//            var elapsedTime = DateTimeOffset.UtcNow - started;
//            var endedEvent = new SubscriptionAccountCreationEndedEvent(subscriptionAccountId, userIdentifier, 100, (int)elapsedTime.TotalSeconds, DateTime.UtcNow);

//            Emit(endedEvent);
//        }
//    }
//}
