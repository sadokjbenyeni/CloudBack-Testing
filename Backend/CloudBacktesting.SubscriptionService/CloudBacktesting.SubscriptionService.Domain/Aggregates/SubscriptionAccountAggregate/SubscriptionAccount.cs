﻿//using Akkatecture.Aggregates;
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
        private string subscriber;
        private int orderId;
        public SubscriptionAccount(SubscriptionAccountId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string subscriber, int orderId)
        {
            Emit(new SubscriptionAccountCreatedEvent(subscriber, orderId));
            return ExecutionResult.Success();
        }
        public IExecutionResult LinkSubscriptionRequest(string subscriptionRequestId, string subscriptionRequestStatus, string subscriptionRequestType)
        {
            Emit(new SubscriptionRequestLinkedEvent(subscriptionRequestId, subscriptionRequestStatus, subscriptionRequestType, this.subscriber, this.orderId+1));
            return ExecutionResult.Success();
        }

        public void Apply(SubscriptionAccountCreatedEvent aggregateEvent)
        {
            this.subscriber = aggregateEvent.Subscriber;
        }

        public void Apply(SubscriptionRequestLinkedEvent aggregateEvent)
        {
            this.orderId = aggregateEvent.OrderId;
        }
    }
}
