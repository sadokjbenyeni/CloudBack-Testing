using EventFlow.Aggregates;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionRequestCreatedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>
    {
        public string Status { get; }
        public string SubscriptionAccountId { get; set; }
        public string Subscriber { get; }
        public string Type { get; }
        public DateTime SubscriptionDate { get; }


        public SubscriptionRequestCreatedEvent(string subscriptionAccountId, string status, string subscriber, string type, DateTime subscriptionDate)
        {
            SubscriptionAccountId = subscriptionAccountId;
            Status = status;
            Subscriber = subscriber;
            Type = type;
            SubscriptionDate = subscriptionDate;
        }
    }
}
