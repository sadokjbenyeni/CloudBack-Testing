using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate
{
    public class SubscriptionRequestValidatedEvent : AggregateEvent<SubscriptionRequest, SubscriptionRequestId>, ISubscriptionSagaRequestId
    {
        public SubscriptionRequestValidatedEvent(string requestId)
        {
            RequestId = requestId;
        }

        public string RequestId { get; }
    }
}