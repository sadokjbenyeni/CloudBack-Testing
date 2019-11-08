using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount.Events;
using CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount
{
    public class SubscriptionAccountState : AggregateState<SubscriptionAccount, SubscriptionAccountId>,
        IApply<SubscriptionCreatedEvent>
    {
        public SubscriptionStatus SubscriptionStatus { get; set; }

        public void Apply(SubscriptionCreatedEvent aggregrateEvent)
        {
            SubscriptionStatus = aggregrateEvent.SubscriptionStatus;
        }
    }
}
