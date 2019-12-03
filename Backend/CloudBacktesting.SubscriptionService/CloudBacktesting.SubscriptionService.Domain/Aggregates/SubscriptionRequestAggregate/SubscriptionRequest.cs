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
        public string subscriptionAccountId;
        private string status;

        public SubscriptionRequest(SubscriptionRequestId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string subscriptionAccountId, string type)
        {
            var @event = new SubscriptionRequestCreatedEvent(this.Id.Value, subscriptionAccountId, "Creating", type);
            
            Emit(@event);
            return ExecutionResult.Success();
        }

        public IExecutionResult ValidateBySystem(string subscriber)
        {
            Emit(new SubscriptionAccountAffectedEvent(this.subscriptionAccountId, subscriber));
            Emit(new SubscriptionRequestStatusUpdatedEvent("Pending"));
            Emit(new SubscriptionRequestValidatedEvent(this.Id.Value));
            return ExecutionResult.Success();
        }

        public IExecutionResult ValidateByAdmin()
        {
            Emit(new SubscriptionRequestStatusUpdatedEvent("Validated"));
            Emit(new SubscriptionRequestValidatedEvent(this.Id.Value));
            return ExecutionResult.Success();
        }

        public void Apply(SubscriptionRequestCreatedEvent @event) 
        {
            this.subscriptionAccountId = @event.SubscriptionAccountId;
        }

        public void Apply(SubscriptionAccountAffectedEvent @event) { }

        public void Apply(SubscriptionRequestStatusUpdatedEvent @event) 
        {
            this.status = @event.Status;
        }
        public void Apply(SubscriptionRequestValidatedEvent @event) { }
    }
}
