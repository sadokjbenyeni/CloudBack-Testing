using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Aggregates;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events
{
    public class SubscriptionRequestManualValidatedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>, ISubscriptionSagaRequestId
    {
        public SubscriptionRequestManualValidatedEvent(string requestId, DateTime manualValidatedDate)
        {
            RequestId = requestId;
            ManualValidatedDate = manualValidatedDate;
        }

        public string RequestId { get; }
        public DateTime ManualValidatedDate { get; }
    }

}
