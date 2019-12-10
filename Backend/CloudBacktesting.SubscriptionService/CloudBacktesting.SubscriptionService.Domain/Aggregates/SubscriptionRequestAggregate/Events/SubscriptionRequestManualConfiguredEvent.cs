using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionRequestManualConfiguredEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>, ISubscriptionSagaRequestId
    {
        public string RequestId { get; }
        public string SubscriptionAccountId { get; }
        public string Subscriber { get; set; }
        public string Message { get; set; }
        public DateTime ActivatedDate { get; set; }

        public SubscriptionRequestManualConfiguredEvent(string requestId, string subscriptionAccountId, string subscriber, string message, DateTime activatedDate)
        {
            RequestId = requestId;
            SubscriptionAccountId = subscriptionAccountId;
            ActivatedDate = activatedDate;
            Message = message;
            Subscriber = subscriber;
        }
    }
}
