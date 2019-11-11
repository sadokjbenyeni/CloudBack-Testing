using System;
using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Repositories.SubscriptionProcessingHistory.Commands
{
    public class AddSubscriptionProcessingHistoryCommand
    {
        public SubscriptionStatus SubscriptionStatus { get; }

        public AddSubscriptionProcessingHistoryCommand(SubscriptionStatus subscriptionStatus)
        {
            if (string.IsNullOrEmpty(subscriptionStatus.Value)) throw new ArgumentNullException(nameof(subscriptionStatus));

            SubscriptionStatus = subscriptionStatus;
        }
    }
}
