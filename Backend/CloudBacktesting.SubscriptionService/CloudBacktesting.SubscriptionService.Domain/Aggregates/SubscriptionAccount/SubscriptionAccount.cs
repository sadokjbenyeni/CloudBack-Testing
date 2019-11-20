//using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount.Events;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount
{
    ///// <Summary>
    ///// this command create a first time the SubscriptionAccount model
    ///// this command can be call only one time by user
    ///// <Summary>
    public class SubscriptionAccount : AggregateRoot<SubscriptionAccount, SubscriptionAccountId>, IEmit<SubscriptionAccountCreatedEvent>

    {
        public SubscriptionAccount(SubscriptionAccountId aggregateId) : base(aggregateId) { }

        public IExecutionResult SetSubscriptionAccount(string subscriber, DateTime subscriptionDate)
        {
            Emit(new SubscriptionAccountCreatedEvent(subscriber, subscriptionDate));
            return ExecutionResult.Success();
        }

        public void Apply(SubscriptionAccountCreatedEvent aggregateEvent) { }
    }
}
