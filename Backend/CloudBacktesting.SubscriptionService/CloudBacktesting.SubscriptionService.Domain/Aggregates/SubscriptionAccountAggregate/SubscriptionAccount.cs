//using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate
{
    ///// <Summary>
    ///// this command create a first time the SubscriptionAccount model
    ///// this command can be call only one time by user
    ///// <Summary>
    public class SubscriptionAccount : AggregateRoot<SubscriptionAccount, SubscriptionAccountId>, IEmit<SubscriptionAccountCreatedEvent>, IEmit<SubscriptionRequestLinkedEvent>

    {
        public SubscriptionAccount(SubscriptionAccountId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string subscriber)
        {
            Emit(new SubscriptionAccountCreatedEvent(subscriber));
            return ExecutionResult.Success();
        }
        public IExecutionResult LinkSubscriptionRequest(string subscriptionRequestId, string subscriptionRequestStatus, string subscriptionRequestType)
        {
            Emit(new SubscriptionRequestLinkedEvent(subscriptionRequestId, subscriptionRequestStatus, subscriptionRequestType));
            return ExecutionResult.Success();
        }

        public void Apply(SubscriptionAccountCreatedEvent aggregateEvent) { }

        public void Apply(SubscriptionRequestLinkedEvent aggregateEvent) { }
    }
}
