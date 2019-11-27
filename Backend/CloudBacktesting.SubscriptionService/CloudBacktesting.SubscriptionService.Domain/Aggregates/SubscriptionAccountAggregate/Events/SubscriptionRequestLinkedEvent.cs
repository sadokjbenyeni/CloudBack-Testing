//using Akkatecture.Aggregates;
using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Events
{
    public class SubscriptionRequestLinkedEvent : AggregateEvent<SubscriptionAccount, SubscriptionAccountId>
    {
        public string SubscriptionRequestId { get; }
        public string SubscriptionRequestStatus { get; }
        public string SubscriptionRequestType { get; }

        public SubscriptionRequestLinkedEvent(string subscriptionRequestId, string subscriptionRequestStatus, string subscriptionRequestType)
        {
            SubscriptionRequestId = subscriptionRequestId;
            SubscriptionRequestStatus = subscriptionRequestStatus;
            SubscriptionRequestType = subscriptionRequestType;
        }
    }
}