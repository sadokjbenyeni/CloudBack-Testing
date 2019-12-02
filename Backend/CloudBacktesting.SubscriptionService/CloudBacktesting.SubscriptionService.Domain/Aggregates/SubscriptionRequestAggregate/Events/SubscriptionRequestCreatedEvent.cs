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

        public SubscriptionRequestCreatedEvent(string requestid, string subscriptionAccountId, string status, string type)
        {
            RequestId = requestid;
            SubscriptionAccountId = subscriptionAccountId;
            Status = status;
            Type = type;
        }
    }
}
