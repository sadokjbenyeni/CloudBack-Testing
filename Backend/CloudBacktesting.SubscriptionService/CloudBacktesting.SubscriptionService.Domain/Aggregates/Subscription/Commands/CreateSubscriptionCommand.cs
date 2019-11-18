using Akka.Actor;
using Akkatecture.Aggregates.ExecutionResults;
using Akkatecture.Commands;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription.Events;
using System;
using System.Collections.Generic;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription.Commands
{
    public class CreateSubscriptionCommand : Command<Subscription, SubscriptionId>
    {
        public string SubscriptionUser { get; }
        public string SubscriptionType { get; }
        public DateTime SubscriptionDate { get; }

        public CreateSubscriptionCommand(SubscriptionId aggregateId, string subscriptionUser, string subscriptionType, DateTime subscriptionDate) : base(aggregateId)
        {
            if (subscriptionUser == null) throw new ArgumentNullException(nameof(subscriptionUser));
            if (subscriptionType == null) throw new ArgumentNullException(nameof(subscriptionType));
            if (subscriptionDate == null) throw new ArgumentNullException(nameof(subscriptionDate));
            SubscriptionUser = subscriptionUser;
            SubscriptionType = subscriptionType;
            SubscriptionDate = subscriptionDate;
        }
    }

    public class CreateSubscriptionCommandHandler : CommandHandler<Subscription, SubscriptionId, CreateSubscriptionCommand>
            {
        public string SubscriptionStatus { get; }
        public string SubscriptionUser { get; }
        public string SubscriptionType { get; }
        public DateTime SubscriptionDate { get; }

        public override void Handle(
            Subscription aggregate,
            IActorContext context,
            CreateSubscriptionCommand command)
        {
            if (aggregate.IsNew)
            {
                var aggregateEvent = new SubscriptionCreatedEvent(SubscriptionStatus, SubscriptionUser, SubscriptionType, SubscriptionDate);
                aggregate.Emit(aggregateEvent);

                var executionResult = new SuccessExecutionResult();
                context.Sender.Tell(executionResult);
            }
            else
            {
                var executionResult = new FailedExecutionResult(new List<string> { "Aggregate is already created" });
                context.Sender.Tell(executionResult);
            }
        }
    }

}
