using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionAccountAffectedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>
    {

        public SubscriptionAccountAffectedEvent(string subscriber)
        {
           
            this.Subscriber = subscriber;
        }
        public string Subscriber { get; set; }
    }
}