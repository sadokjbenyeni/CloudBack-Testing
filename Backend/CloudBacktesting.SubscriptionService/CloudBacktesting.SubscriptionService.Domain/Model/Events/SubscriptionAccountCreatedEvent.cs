using Akkatecture.Aggregates;
using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;
using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Events
{
    public class SubscriptionAccountCreatedEvent : AggregateEvent<SubscriptionAccount, SubscriptionAccountId>
    {
        public SubscriptionUser SubscriptionUser { get; }
        public SubscriptionDate SubscriptionDate { get; }

        public SubscriptionAccountCreatedEvent(SubscriptionUser subscriptionUser,SubscriptionDate subscriptionDate )
        {
            SubscriptionUser = subscriptionUser;
            SubscriptionDate = subscriptionDate;
        }

    }
}
