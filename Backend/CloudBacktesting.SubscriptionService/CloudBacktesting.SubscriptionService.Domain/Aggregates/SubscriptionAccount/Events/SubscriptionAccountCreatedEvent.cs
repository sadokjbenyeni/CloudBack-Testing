using System;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount.Events
{

    public class SubscriptionAccountCreatedEvent /*: AggregateEvent<SubscriptionAccount, SubscriptionAccountId>*/
    {
        public string SubscriptionUser { get; }
        public DateTime SubscriptionDate { get; }

        public SubscriptionAccountCreatedEvent(string subscriptionUser, DateTime subscriptionDate)
        {
            SubscriptionUser = subscriptionUser;
            SubscriptionDate = subscriptionDate;
        }

    }
}
