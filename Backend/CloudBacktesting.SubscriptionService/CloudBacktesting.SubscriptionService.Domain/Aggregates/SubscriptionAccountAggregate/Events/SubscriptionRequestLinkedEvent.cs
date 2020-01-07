//using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Events
{
    public class SubscriptionRequestLinkedEvent : AggregateEvent<SubscriptionAccount, SubscriptionAccountId>, ISubscriptionSagaRequestId
    {
        public string SubscriptionRequestStatus { get; }
        public string SubscriptionRequestType { get; }
        public string Subscriber { get; }
        public string RequestId { get; }
        public int OrderId { get; }


        public SubscriptionRequestLinkedEvent(string requestId, string subscriptionRequestStatus, string subscriptionRequestType, string subscriber, int orderId)
        {
            RequestId = requestId;
            SubscriptionRequestStatus = subscriptionRequestStatus;
            SubscriptionRequestType = subscriptionRequestType;
            Subscriber = subscriber;
            OrderId = orderId;
        }
    }
}