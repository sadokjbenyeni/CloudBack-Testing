using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionRequestStatusUpdatedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>, ISubscriptionSagaRequestId
    {
        public string Status { get; }
        public string RequestId { get; set; }

        public SubscriptionRequestStatusUpdatedEvent(string status, string requestId)
        {  
            Status = status;
            RequestId = requestId;
        }
    }
}