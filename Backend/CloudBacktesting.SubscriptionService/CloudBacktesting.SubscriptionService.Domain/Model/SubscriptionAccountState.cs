using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Model.Events;
using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model
{
    public class SubscriptionAccountState : AggregateState<SubscriptionAccount, SubscriptionAccountId>,
        IApply<SubscriptionCreatedEvent>
    {
        public SubscriptionStatus SubscriptionStatus { get; set; }
        public SubscriptionUser SubscriptionUser { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public SubscriptionDate SubscriptionDate { get; set; }


        public void Apply(SubscriptionCreatedEvent aggregrateEvent)
        {
            SubscriptionStatus = aggregrateEvent.SubscriptionStatus;
            SubscriptionUser = aggregrateEvent.SubscriptionUser;
            SubscriptionType = aggregrateEvent.SubscriptionType;
            SubscriptionDate = aggregrateEvent.SubscriptionDate;
        }
    }
}
