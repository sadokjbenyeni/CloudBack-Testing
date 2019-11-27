using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionStatusUpdatedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>
    {
        public string Status { get; }

        public SubscriptionStatusUpdatedEvent(string status)
        {
            Status = status;
        }
    }
}