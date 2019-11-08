
using Akkatecture.Entities;
using CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount.Entities
{
    public class SubscriptionProcessing : Entity<SubscriptionProcessingId>
    {
        public SubscriptionAccountId SubscriptionAccount { get; }
        public SubscriptionAccountId SubscriptionAdministratorAccount { get; }

        public SubscriptionStatus SubscriptionStatus { get; }

        public SubscriptionProcessing(
            SubscriptionProcessingId subscriptionProcessingId,
            SubscriptionAccountId subscriptionAccount,
             SubscriptionAccountId subscriptionAdministratorAccount,
            SubscriptionStatus subscriptionStatus)
            : base(subscriptionProcessingId)
        {
            SubscriptionAdministratorAccount = subscriptionAdministratorAccount;
            SubscriptionAccount = subscriptionAccount;
            SubscriptionStatus = subscriptionStatus;
        }

        public SubscriptionProcessing(
            SubscriptionAccountId subscriptionAccount,
            SubscriptionAccountId subscriptionAdministratorAccount,
            SubscriptionStatus subscriptionStatus)
            : this(SubscriptionProcessingId.New, subscriptionAccount, subscriptionAdministratorAccount, subscriptionStatus)
        {
        }
    }
}
