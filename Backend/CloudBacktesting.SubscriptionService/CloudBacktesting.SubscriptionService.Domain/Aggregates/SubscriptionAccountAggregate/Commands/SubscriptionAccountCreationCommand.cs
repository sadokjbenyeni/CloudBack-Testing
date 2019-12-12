using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;


namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands
{
    public class SubscriptionAccountCreationCommand : Command<SubscriptionAccount, SubscriptionAccountId, IExecutionResult>
    {
        public string Subscriber { get; set; }
        public int OrderId { get; set; }
        public SubscriptionAccountCreationCommand(string subscriber, int orderId) : base(SubscriptionAccountId.New)
        {
            Subscriber = subscriber ?? throw new ArgumentNullException(nameof(subscriber));
            OrderId = orderId;
        }
    }
}
