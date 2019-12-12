using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Aggregates;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionRequestCreatedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>, ISubscriptionSagaRequestId
    {
        public string Status { get; }
        public string SubscriptionAccountId { get; }
        public string Type { get; }
        public DateTime CreationDate { get; }
        public string RequestId { get; }
        public int OrderId { get; }

        public SubscriptionRequestCreatedEvent(string requestId, string subscriptionAccountId, string status, string type, DateTime creationDate, int orderId)
        {
            OrderId = orderId;
            RequestId = requestId;
            SubscriptionAccountId = subscriptionAccountId;
            Status = status;
            Type = type;
            CreationDate = creationDate;
        }
    }
}
