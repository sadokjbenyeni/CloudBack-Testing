//using Akkatecture.Aggregates;
using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Events
{
    public class SubscriptionRequestLinkedEvent : AggregateEvent<SubscriptionAccount, SubscriptionAccountId>
    {
        public string SubscriptionRequestId { get; }
        public string SubscriptionRequestStatus { get; }
        public string SubscriptionRequestType { get; }
        public string Subscriber { get; }

        public SubscriptionRequestLinkedEvent(string subscriptionRequestId, string subscriptionRequestStatus, string subscriptionRequestType, string subscriber)
        {
            SubscriptionRequestId = subscriptionRequestId;
            SubscriptionRequestStatus = subscriptionRequestStatus;
            SubscriptionRequestType = subscriptionRequestType;
            Subscriber = subscriber;
        }
    }
}