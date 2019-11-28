using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using EventFlow.Aggregates;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionRequestCreatedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>
    {
        public string Status { get; }
        public string SubscriptionAccountId { get; }
        public string Subscriber { get; }
        public string Type { get; }


        public SubscriptionRequestCreatedEvent(string subscriptionAccountId, string status, string subscriber, string type)
        {
            SubscriptionAccountId = subscriptionAccountId;
            Status = status;
            Subscriber = subscriber;
            Type = type;
        }
    }
}
