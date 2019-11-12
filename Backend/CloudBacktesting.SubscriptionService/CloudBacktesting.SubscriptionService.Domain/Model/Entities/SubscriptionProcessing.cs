
using Akkatecture.Entities;
using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Entities
{
    public class SubscriptionProcessing : Entity<SubscriptionProcessingId>
    {
        public SubscriptionAccountId SubscriptionAccount { get; }
        public SubscriptionAccountId SubscriptionAdministratorAccount { get; }

        public SubscriptionStatus SubscriptionStatus { get; }
        public SubscriptionUser SubscriptionUser { get; }
        public SubscriptionType SubscriptionType { get; }
        public SubscriptionDate SubscriptionDate { get; }

        public SubscriptionProcessing(
            SubscriptionProcessingId subscriptionProcessingId,
            SubscriptionAccountId subscriptionAccount,
            SubscriptionAccountId subscriptionAdministratorAccount,
            SubscriptionStatus subscriptionStatus,
            SubscriptionUser subscriptionUser,
            SubscriptionType subscriptionType,
            SubscriptionDate subscriptionDate)
            : base(subscriptionProcessingId)
        {
            SubscriptionAdministratorAccount = subscriptionAdministratorAccount;
            SubscriptionAccount = subscriptionAccount;
            SubscriptionStatus = subscriptionStatus;
            SubscriptionUser = subscriptionUser;
            SubscriptionType = subscriptionType;
            SubscriptionDate = subscriptionDate;
        }

        public SubscriptionProcessing(
            SubscriptionAccountId subscriptionAccount,
            SubscriptionAccountId subscriptionAdministratorAccount,
            SubscriptionStatus subscriptionStatus,
            SubscriptionUser subscriptionUser,
            SubscriptionType subscriptionType,
            SubscriptionDate subscriptionDate)
            : this(SubscriptionProcessingId.New, subscriptionAccount, subscriptionAdministratorAccount, subscriptionStatus, subscriptionUser, subscriptionType, subscriptionDate)
        {
        }
    }
}
