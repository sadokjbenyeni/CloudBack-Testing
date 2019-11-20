//using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount.Events;
//using System;


//namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionAccountSaga
//{
//    public class SubscriptionAccountCreationSagaLocator /*: ISagaLocator<SubscriptionAccountCreationSagaId>*/
//    {
//        public const string LocatorIdPrefix = "resourcecreation";
//        public SubscriptionAccountCreationSagaId LocateSaga(IDomainEvent domainEvent)
//        {
//            switch (domainEvent.GetAggregateEvent())
//            {
//                case SubscriptionAccountCreatedEvent _:
//                    return new SubscriptionAccountCreationSagaId($"{LocatorIdPrefix}-{domainEvent.GetIdentity()}");
//                default:
//                    throw new ArgumentException(nameof(domainEvent));
//            }
//        }
//    }
//}
