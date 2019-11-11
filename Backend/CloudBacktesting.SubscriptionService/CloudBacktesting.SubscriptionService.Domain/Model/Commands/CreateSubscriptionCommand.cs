using Akkatecture.Commands;
using System;
using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Commands
{
    public class CreateSubscriptionCommand : Command<SubscriptionAccount, SubscriptionAccountId>
    {
        public SubscriptionStatus SubscriptionState { get; }
        public CreateSubscriptionCommand( SubscriptionAccountId aggregateId, SubscriptionStatus subscriptionState) : base(aggregateId)
        {
            if (subscriptionState == null) throw new ArgumentNullException(nameof(subscriptionState));

            SubscriptionState = subscriptionState;
        }
    }
}
