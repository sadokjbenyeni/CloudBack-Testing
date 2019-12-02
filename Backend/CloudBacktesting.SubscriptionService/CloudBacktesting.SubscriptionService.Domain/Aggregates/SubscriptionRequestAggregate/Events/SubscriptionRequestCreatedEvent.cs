using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionRequestCreatedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>, ISubscriptionSagaRequestId
    {
        public string Status { get; }
        public string SubscriptionAccountId { get; }
        public string Type { get; }
        public string RequestId { get; }

        public SubscriptionRequestCreatedEvent(string requestId, string subscriptionAccountId, string status, string type)
        {
            RequestId = requestId;
            SubscriptionAccountId = subscriptionAccountId;
            Status = status;
            Type = type;
        }
    }
}
