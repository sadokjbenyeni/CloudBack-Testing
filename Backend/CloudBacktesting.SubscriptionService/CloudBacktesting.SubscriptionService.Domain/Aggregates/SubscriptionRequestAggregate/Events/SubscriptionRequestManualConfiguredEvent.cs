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
        public DateTime ManualConfiguredDate { get; set; }

        public SubscriptionRequestManualConfiguredEvent(string requestId, string subscriptionAccountId, string subscriber, DateTime manualConfiguredDate)
        {
            RequestId = requestId;
            SubscriptionAccountId = subscriptionAccountId;
            ManualConfiguredDate = manualConfiguredDate;
            Subscriber = subscriber;
        }
    }
}
