//using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Events
{
    public class SubscriptionRequestLinkedEvent : AggregateEvent<SubscriptionAccount, SubscriptionAccountId>, ISubscriptionSagaRequestId
    //AggregateEvent<SubscriptionCreationSaga, SubscriptionCreationSagaId>
    {
        //public string SubscriptionRequestId { get; }
        public string SubscriptionRequestStatus { get; }
        public string SubscriptionRequestType { get; }
        public string Subscriber { get; }
        public string RequestId { get; }


        public SubscriptionRequestLinkedEvent(string requestId, string subscriptionRequestStatus, string subscriptionRequestType, string subscriber)
        {
            RequestId = requestId;
            SubscriptionRequestStatus = subscriptionRequestStatus;
            SubscriptionRequestType = subscriptionRequestType;
            Subscriber = subscriber;
        }
    }
}