using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount.Commands;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount
{
    ///// <Summary>
    ///// this command create a first time the SubscriptionAccount model
    ///// this command can be call only one time by user
    ///// <Summary>
    public class SubscriptionAccount : AggregateRoot<SubscriptionAccount, SubscriptionAccountId, SubscriptionAccountState>

    {
        public SubscriptionAccount(SubscriptionAccountId aggregateId) : base(aggregateId)

        {
            Command<CreateSubscriptionAccountCommand, CreateSubscriptionAccountCommandHandler>();
        }
    }
}
