using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
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
        public SubscriptionRequest(SubscriptionRequestId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string subscriptionAccountId, string status, string subscriber, string type)
        {
            Emit(new SubscriptionRequestCreatedEvent(subscriptionAccountId, status, subscriber, type));

            return ExecutionResult.Success();
        }

        public IExecutionResult ValidateBySystem()
        {
            Emit(new SubscriptionStatusUpdatedEvent("Pending"));
            Emit(new SubscriptionRequestValidatedEvent());
            return ExecutionResult.Success();
        }

        public void Apply(SubscriptionRequestCreatedEvent aggregateEvent) { }
    }
}
