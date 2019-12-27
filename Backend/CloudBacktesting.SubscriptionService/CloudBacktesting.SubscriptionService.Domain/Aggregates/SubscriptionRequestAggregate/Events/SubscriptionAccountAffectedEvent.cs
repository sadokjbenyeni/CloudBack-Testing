using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionAccountAffectedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>
    {

        public SubscriptionAccountAffectedEvent(string subscriber, int orderId)
        {
            OrderId = orderId;
            Subscriber = subscriber;
        }

        public int OrderId { get; set; }
        public string Subscriber { get; set; }
    }
}