using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionRequestManualDeclinedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>, ISubscriptionSagaRequestId
    {
        public string RequestId { get; }
        public string SubscriptionAccountId { get; }
        public string Message { get; }
        public DateTime ManualDeclinedDate { get; }
        public SubscriptionRequestManualDeclinedEvent(string requestId, string subscriptionAccountId, string message, DateTime manualDeclinedDate)
        {
            RequestId = requestId;
            SubscriptionAccountId = subscriptionAccountId;
            Message = message;
            ManualDeclinedDate = manualDeclinedDate;
        }
    }
}
