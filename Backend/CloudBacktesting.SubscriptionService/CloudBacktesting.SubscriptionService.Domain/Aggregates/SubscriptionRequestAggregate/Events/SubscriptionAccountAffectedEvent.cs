using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionAccountAffectedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>
    {

        public SubscriptionAccountAffectedEvent(string subscriptionAccountId, string subscriber)
        {
            this.SubscriptionAccountId = subscriptionAccountId;
            this.Subscriber = subscriber;
        }

        public string SubscriptionAccountId { get; set; }
        public string Subscriber { get; set; }
    }
}