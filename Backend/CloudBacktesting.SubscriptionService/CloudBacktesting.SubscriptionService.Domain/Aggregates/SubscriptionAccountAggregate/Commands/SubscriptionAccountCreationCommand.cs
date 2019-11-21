using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;


namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands
{
    public class SubscriptionAccountCreationCommand : Command<SubscriptionAccount, SubscriptionAccountId, IExecutionResult>
    {
        public string Subscriber { get; set; }
        public DateTime SubscriptionDate { get; set; }

        public SubscriptionAccountCreationCommand(SubscriptionAccountId aggregateId, string subscriber, DateTime subscriptionDate) : base(aggregateId)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));
            if (subscriptionDate == null) throw new ArgumentNullException(nameof(subscriptionDate));

            Subscriber = subscriber;
            SubscriptionDate = subscriptionDate;
        }
    }
}
