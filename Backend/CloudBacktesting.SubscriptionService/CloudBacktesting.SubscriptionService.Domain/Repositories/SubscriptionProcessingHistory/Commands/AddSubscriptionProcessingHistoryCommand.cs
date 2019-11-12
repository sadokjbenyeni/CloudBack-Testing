using System;
using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Repositories.SubscriptionProcessingHistory.Commands
{
    public class AddSubscriptionProcessingHistoryCommand
    {
        public SubscriptionStatus SubscriptionStatus { get; }
        public SubscriptionUser SubscriptionUser { get; }
        public SubscriptionType SubscriptionType { get; }
        public SubscriptionDate SubscriptionDate { get; }

        public AddSubscriptionProcessingHistoryCommand(SubscriptionStatus subscriptionStatus, SubscriptionUser subscriptionUser, SubscriptionType subscriptionType, SubscriptionDate subscriptionDate)
        {
            if (string.IsNullOrEmpty(subscriptionStatus.Value)) throw new ArgumentNullException(nameof(subscriptionStatus));
            if (string.IsNullOrEmpty(subscriptionUser.Value)) throw new ArgumentNullException(nameof(subscriptionStatus));
            if (string.IsNullOrEmpty(subscriptionType.Value)) throw new ArgumentNullException(nameof(subscriptionStatus));
            if (subscriptionDate.Value == null) throw new ArgumentNullException(nameof(subscriptionStatus));


            SubscriptionStatus = subscriptionStatus;
            SubscriptionUser = subscriptionUser;
            SubscriptionType = subscriptionType;
            SubscriptionDate = subscriptionDate;
        }
    }
}
