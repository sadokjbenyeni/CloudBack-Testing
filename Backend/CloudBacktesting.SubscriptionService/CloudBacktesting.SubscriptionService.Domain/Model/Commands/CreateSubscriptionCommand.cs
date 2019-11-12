using Akkatecture.Commands;
using System;
using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Commands
{
    public class CreateSubscriptionCommand : Command<SubscriptionAccount, SubscriptionAccountId>
    {
        public SubscriptionStatus SubscriptionStatus { get; }
        public SubscriptionUser SubscriptionUser { get; }
        public SubscriptionType SubscriptionType { get; }
        public SubscriptionDate SubscriptionDate { get; }
        public CreateSubscriptionCommand( SubscriptionAccountId aggregateId, SubscriptionStatus subscriptionStatus, SubscriptionUser subscriptionUser, SubscriptionType subscriptionType, SubscriptionDate subscriptionDate) : base(aggregateId)
        {
            if (subscriptionStatus == null) throw new ArgumentNullException(nameof(subscriptionStatus));
            if (subscriptionUser == null) throw new ArgumentNullException(nameof(subscriptionUser));
            if (subscriptionType == null) throw new ArgumentNullException(nameof(subscriptionType));
            if (subscriptionDate == null) throw new ArgumentNullException(nameof(subscriptionDate));

            SubscriptionStatus = subscriptionStatus;
            SubscriptionUser = subscriptionUser;
            SubscriptionType = subscriptionType;
            SubscriptionDate = subscriptionDate;
        }
    }
}
