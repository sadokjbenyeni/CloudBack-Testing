////using Akkatecture.Aggregates;
////using Akkatecture.Subscribers;
//using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionSaga;
//using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionSaga.Events;
//using System.Collections.Generic;
//using System.Threading.Tasks;
////using static CloudBacktesting.SubscriptionService.Domain.Repositories.Subscriptions.SubscriptionsQueryHandler;

//namespace CloudBacktesting.SubscriptionService.Domain.Repositories.Subscriptions
//{

//    public class SubscriptionsStorageHandler : DomainEventSubscriber,
//         ISubscribeToAsync<SubscriptionCreationSaga, SubscriptionCreationSagaId, SubscriptionCreationEndedEvent>
//    {
//        private readonly List<SubscriptionsProjection> _subscriptionsProjection = new List<SubscriptionsProjection>();

//        public SubscriptionsStorageHandler()
//        {
//            Receive<GetSubscriptionsQuery>(Handle);
//        }
//        public Task HandleAsync(IDomainEvent<SubscriptionCreationSaga, SubscriptionCreationSagaId, SubscriptionCreationEndedEvent> domainEvent)
//        {
//            var readModel = new SubscriptionsProjection(
//                domainEvent.AggregateEvent.Id,
//                domainEvent.AggregateEvent.SubscriptionAccountId,
//                domainEvent.AggregateEvent.UserIdentifier,
//                domainEvent.AggregateEvent.Elapsed,
//                domainEvent.AggregateEvent.EndedAt,
//                domainEvent.AggregateEvent.ValidatorUserIdentifier,
//                domainEvent.AggregateEvent.EnvironmentSetupUserIdentifier,
//                domainEvent.AggregateEvent.TypeOfSubscription,
//                domainEvent.AggregateEvent.Status);

//            _subscriptionsProjection.Add(readModel);

//            return Task.CompletedTask;
//        }

//        public bool Handle(GetSubscriptionsQuery query)
//        {
//            Sender.Tell(_subscriptionsProjection, Self);
//            return true;
//        }
//    }
//}
