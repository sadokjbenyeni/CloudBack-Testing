using Akkatecture.Commands;
using CloudBacktesting.SubscriptionService.Domain.Model.Subscription.ValueObjects;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Subscription.Commands
{
    public class AskNewSubscriptionCommand : Command<Account, AccountId>
    {
        public State OpeningState { get; }
        public AskNewSubscriptionCommand( AccountId aggregateId, State openingState) : base(aggregateId)
        {
            if (openingState == null) throw new ArgumentNullException(nameof(openingState));

            OpeningState = openingState;
        }
    }
}
