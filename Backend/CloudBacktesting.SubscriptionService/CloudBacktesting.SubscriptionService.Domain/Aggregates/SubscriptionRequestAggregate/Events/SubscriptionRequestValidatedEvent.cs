using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate
{
    public class SubscriptionRequestValidatedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>
    {
        public SubscriptionRequestValidatedEvent()
        {

        }
    }
}