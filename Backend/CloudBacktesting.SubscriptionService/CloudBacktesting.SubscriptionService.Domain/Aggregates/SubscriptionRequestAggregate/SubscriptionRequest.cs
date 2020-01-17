using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Events;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository;
using CloudBacktesting.SubscriptionService.Domain.Specifications;
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
        public string status;
        public SubscriptionRequest(SubscriptionRequestId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string subscriptionAccountId, string type, string paymentMethodId)
        {
            var spec = new IsNullOrEmptyAccountIdSpecification();
            if (spec.IsSatisfiedBy(subscriptionAccountId) == false)
            {
                return ExecutionResult.Failed(spec.WhyIsNotSatisfiedBy("Account Id Is Null Or Empty"));
            }
            var @event = new SubscriptionRequestCreatedEvent(this.Id.Value, subscriptionAccountId, "Creating", type, DateTime.UtcNow, paymentMethodId);
            Emit(@event);
            return ExecutionResult.Success();

        }

        public IExecutionResult ValidateBySystem(SubscriptionRequestId subscriptionRequestId, string subscriber, int orderId)
        {
            Emit(new SubscriptionAccountAffectedEvent(subscriber, orderId));
            Emit(new SubscriptionRequestStatusUpdatedEvent("Pending", subscriptionRequestId.ToString()));
            Emit(new SubscriptionRequestValidatedEvent(this.Id.Value));
            return ExecutionResult.Success();
        }

        public IExecutionResult RejectBySystem(SubscriptionRequestId subscriptionRequestId, string subscriber, int orderId)
        {
            Emit(new SubscriptionRequestStatusUpdatedEvent("Rejected", subscriptionRequestId.ToString()));
            Emit(new SubscriptionRequestRejectedEvent(this.Id.Value, this.subscriptionAccountId, subscriber, orderId, "You have been rejected by the system", DateTime.UtcNow));
            return ExecutionResult.Success();
        }
        public IExecutionResult ManualValidate(SubscriptionRequestId subscriptionRequestId)
        {
            Emit(new SubscriptionRequestStatusUpdatedEvent("Validated", subscriptionRequestId.ToString()));
            Emit(new SubscriptionRequestManualValidatedEvent(this.Id.Value, DateTime.UtcNow));
            return ExecutionResult.Success();
        }

        public IExecutionResult ManualDecline(SubscriptionRequestId subscriptionRequestId, string message)
        {
            Emit(new SubscriptionRequestStatusUpdatedEvent("Declined", subscriptionRequestId.ToString()));
            Emit(new SubscriptionRequestManualDeclinedEvent(this.Id.Value, this.subscriptionAccountId, message, DateTime.UtcNow));
            return ExecutionResult.Success();
        }

        public IExecutionResult ManualConfigure(SubscriptionRequestId subscriptionRequestId, string message)
        {
            Emit(new SubscriptionRequestStatusUpdatedEvent("Active", subscriptionRequestId.ToString()));
            Emit(new SubscriptionRequestManualConfiguredEvent(this.Id.Value, this.subscriptionAccountId, message, DateTime.UtcNow));
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
        public void Apply(SubscriptionRequestRejectedEvent @event) { }
        public void Apply(SubscriptionRequestManualValidatedEvent @event) { }
        public void Apply(SubscriptionRequestManualDeclinedEvent @event) { }
        public void Apply(SubscriptionRequestManualConfiguredEvent @event) { }
    }
}
