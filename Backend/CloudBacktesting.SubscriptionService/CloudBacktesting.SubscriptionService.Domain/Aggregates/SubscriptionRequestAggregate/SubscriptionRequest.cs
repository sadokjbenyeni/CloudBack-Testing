using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate
{
    /// <Summary>
    /// This command create a new subscription for one user
    /// <Summary>
    public class SubscriptionRequest : AggregateRoot<SubscriptionRequest, SubscriptionRequestId>, IEmit<SubscriptionRequestCreatedEvent>
    {
        public SubscriptionRequest(SubscriptionRequestId id) : base(id) { }

        public IExecutionResult SetSubscriptionRequest(string status, string subscriber, string type, DateTime subscriptionDate)
        {
            Emit(new SubscriptionRequestCreatedEvent(status, subscriber, type, subscriptionDate));

            return ExecutionResult.Success();
        }

        public void Apply(SubscriptionRequestCreatedEvent aggregateEvent) { }
    }
}
