using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionRequestRejectedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>, ISubscriptionSagaRequestId
    {
        public string RequestId { get; }
        public string SubscriptionAccountId { get; }
        public string Subscriber { get; set; }
        public string Message { get; set; }
        public DateTime SystemRejectedDate { get; }

        public SubscriptionRequestRejectedEvent(string requestId, string subscriptionAccountId, string subscriber, string message, DateTime systemRejectedDate)
        {
            RequestId = requestId;
            Subscriber = subscriber;
            Message = message;
            SubscriptionAccountId = subscriptionAccountId;
            SystemRejectedDate = systemRejectedDate;
        }

    }
}
