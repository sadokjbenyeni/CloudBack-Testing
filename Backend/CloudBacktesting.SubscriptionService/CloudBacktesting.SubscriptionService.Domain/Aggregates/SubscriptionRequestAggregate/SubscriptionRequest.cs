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
        public string SubscriptionAccountId { get; private set; }

        public SubscriptionRequest(SubscriptionRequestId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string subscriptionAccountId, string type)
        {
            Emit(new SubscriptionRequestCreatedEvent(subscriptionAccountId, "Creating", type));

            return ExecutionResult.Success();
        }

        public IExecutionResult ValidateBySystem(string subscriber)
        {
            Emit(new SubscriptionAccountAffectedEvent(this.SubscriptionAccountId, subscriber));
            Emit(new SubscriptionRequestStatusUpdatedEvent("Pending"));
            Emit(new SubscriptionRequestValidatedEvent());
            return ExecutionResult.Success();
        }

        public void Apply(SubscriptionRequestCreatedEvent aggregateEvent) 
        {
            this.SubscriptionAccountId = aggregateEvent.SubscriptionAccountId;
        }
    }
}
