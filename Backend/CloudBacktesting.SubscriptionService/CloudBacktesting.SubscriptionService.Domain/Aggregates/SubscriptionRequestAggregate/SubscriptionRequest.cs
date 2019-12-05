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
            var @event = new SubscriptionRequestCreatedEvent(this.Id.Value, subscriptionAccountId, "Creating", type, DateTime.UtcNow);

            Emit(@event);
            return ExecutionResult.Success();
        }

        public IExecutionResult ValidateBySystem(string subscriber)
        {
            Emit(new SubscriptionAccountAffectedEvent(subscriber));
            Emit(new SubscriptionRequestStatusUpdatedEvent("Pending"));
            Emit(new SubscriptionRequestValidatedEvent(this.Id.Value));
            return ExecutionResult.Success();
        }

        public IExecutionResult ManualValidate()
        {
            Emit(new SubscriptionRequestStatusUpdatedEvent("Validated"));
            Emit(new SubscriptionRequestManualValidatedEvent(this.Id.Value, DateTime.UtcNow));
            return ExecutionResult.Success();
        }

        public IExecutionResult ManualDecline(string message)
        {
            Emit(new SubscriptionRequestStatusUpdatedEvent("Declined"));
            Emit(new SubscriptionRequestManualDeclinedEvent(this.Id.Value, message, DateTime.UtcNow));
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
        public void Apply(SubscriptionRequestManualValidatedEvent @event) { }
        public void Apply(SubscriptionRequestManualDeclinedEvent @event) { }
    }
}
