//using Akkatecture.Aggregates;
//using Akkatecture.Sagas;
//using Akkatecture.Sagas.AggregateSaga;
//using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription;
//using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription.Events;
//using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionSaga.Events;

//using System;
//using System.Threading.Tasks;

//namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionSaga
//{
//    public class SubscriptionCreationSaga : AggregateSaga<SubscriptionCreationSaga, SubscriptionCreationSagaId, SubscriptionCreationSagaState>,
//       ISagaIsStartedByAsync<Subscription, SubscriptionId, SubscriptionCreatedEvent>
//    {
//        public Task HandleAsync(IDomainEvent<Subscription, SubscriptionId, SubscriptionCreatedEvent> domainEvent)
//        {
//            //simulates a long running process
//            var subscriptionId = domainEvent.AggregateIdentity;
//            var startedEvent = new SubscriptionCreationStartedEvent(subscriptionId, DateTime.UtcNow);
//            var started = DateTimeOffset.UtcNow;
//            Emit(startedEvent);

//            // var rng = new Random();
//            // var progress = 0;

//            // while (progress < 100)
//            // {
//            //     var delay = rng.Next(1, 3);

//            //     await Task.Delay(delay * 1000);
//            //     progress += rng.Next(5, 15);
//            //     var elapsed = DateTimeOffset.UtcNow - started;
//            //     var progressEvent = new SubscriptionCreationProgressEvent(subscriptionId, progress, (int)elapsed.TotalSeconds, DateTime.UtcNow);
//            //     Emit(progressEvent);
//            // }

//            var elapsedTime = DateTimeOffset.UtcNow - started;
//            var endedEvent = new SubscriptionCreationEndedEvent(subscriptionId, 100, (int)elapsedTime.TotalSeconds, DateTime.UtcNow);

//            Emit(endedEvent);
//            return Task.CompletedTask;
//        }
//    }
//}
