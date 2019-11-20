using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription.Events;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount
{
    public class SubscriptionAccountState /*: AggregateState<SubscriptionAccount, SubscriptionAccountId>,*/
        //IApply<SubscriptionCreatedEvent>
    {
        public string SubscriptionUser { get; set; }
        public DateTime SubscriptionDate { get; set; }


        public void Apply(SubscriptionCreatedEvent aggregrateEvent)
        {
            SubscriptionUser = aggregrateEvent.SubscriptionUser;
            SubscriptionDate = aggregrateEvent.SubscriptionDate;
        }
    }
}
