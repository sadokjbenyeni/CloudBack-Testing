//using Akkatecture.Aggregates;
//using Akkatecture.Sagas;
//using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription.Events;
//using System;


//namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionSaga
//{
//    public class SubscriptionCreationSagaLocator : ISagaLocator<SubscriptionCreationSagaId>
//    {
//        public const string LocatorIdPrefix = "resourcecreation";
//        public SubscriptionCreationSagaId LocateSaga(IDomainEvent domainEvent)
//        {
//            switch (domainEvent.GetAggregateEvent())
//            {
//                case SubscriptionCreatedEvent _:
//                    return new SubscriptionCreationSagaId($"{LocatorIdPrefix}-{domainEvent.GetIdentity()}");
//                default:
//                    throw new ArgumentException(nameof(domainEvent));
//            }
//        }
//    }
//}
