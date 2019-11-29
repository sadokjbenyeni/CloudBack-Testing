using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionRequestStatusUpdatedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>
    {
        public string Status { get; }

        public SubscriptionRequestStatusUpdatedEvent(string status)
        {
            Status = status;
        }
    }
}