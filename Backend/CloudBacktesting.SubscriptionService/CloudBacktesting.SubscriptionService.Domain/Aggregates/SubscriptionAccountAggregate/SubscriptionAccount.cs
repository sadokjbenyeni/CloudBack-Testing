//using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using CloudBacktesting.SubscriptionService.Domain.Specifications;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate
{
    public class SubscriptionAccount : AggregateRoot<SubscriptionAccount, SubscriptionAccountId>, IEmit<SubscriptionAccountCreatedEvent>, IEmit<SubscriptionRequestLinkedEvent>

    {
        private string subscriber;
        private int orderId = 0;
        public SubscriptionAccount(SubscriptionAccountId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string subscriber)
        {
            Emit(new SubscriptionAccountCreatedEvent(subscriber));
            return ExecutionResult.Success();
        }
        public IExecutionResult LinkSubscriptionRequest(string subscriptionRequestId, string subscriptionRequestStatus, string subscriptionRequestType)
        {
            Emit(new SubscriptionRequestLinkedEvent(subscriptionRequestId, subscriptionRequestStatus, subscriptionRequestType, this.subscriber, this.orderId + 1));
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
