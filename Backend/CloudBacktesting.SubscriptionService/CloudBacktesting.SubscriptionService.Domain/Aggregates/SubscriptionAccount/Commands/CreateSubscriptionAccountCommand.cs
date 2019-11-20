using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription.Commands;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount.Events;
using System;
using System.Collections.Generic;


namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount.Commands
{
    public class CreateSubscriptionAccountCommand /*: Command<SubscriptionAccount, SubscriptionAccountId>*/
    {
        public string SubscriptionUser { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public CreateSubscriptionAccountCommand(SubscriptionAccountId aggregateId, string subscriptionUser, DateTime subscriptionDate) /*: base(aggregateId)*/
        {
            if (subscriptionUser == null) throw new ArgumentNullException(nameof(subscriptionUser));
            if (subscriptionDate == null) throw new ArgumentNullException(nameof(subscriptionDate));
            SubscriptionUser = subscriptionUser;
            SubscriptionDate = subscriptionDate;
        }
    }

    public class CreateSubscriptionAccountCommandHandler /*: CommandHandler<SubscriptionAccount, SubscriptionAccountId, CreateSubscriptionAccountCommand>*/
    {
        public string SubscriptionUser { get; }
        public DateTime SubscriptionDate { get; }

        //public override void Handle(SubscriptionAccount aggregate, /*IActorContext context,*/ CreateSubscriptionAccountCommand command)
        //{
        //    if (!IsValidCommand(aggregate))
        //    {
        //        context.Sender.Tell(new FailedExecutionResult(new[] { "The account subscription is already created" }));
        //        return;
        //    }
        //    var aggregateEvent = new SubscriptionAccountCreatedEvent(SubscriptionUser, SubscriptionDate);
        //    aggregate.Emit(aggregateEvent);
        //    context.Sender.Tell(new SuccessExecutionResult());
        //}

        //private static bool IsValidCommand(SubscriptionAccount aggregate) => aggregate.IsNew;
    }
}
