using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount;
using System;


namespace CloudBacktesting.SubscriptionService.Domain.Repositories.Subscriptions
{
    public class SubscriptionsProjection
    {
        public SubscriptionId Id { get; set; }
        public SubscriptionAccountId SubscriptionAccountId { get; set; }
        public string UserIdentifier { get; set; }
        public double ElapsedTimeToCreation { get; }
        public DateTime CreationDate { get; set; }
        public string ValidatorUserIdentifier { get; set; }
        public string EnvironmentSetupUserIdentifier { get; set; }
        public string TypeOfSubscription { get; set; }
        public string Status { get; set; }

        public SubscriptionsProjection(
            SubscriptionId id,
            SubscriptionAccountId subscriptionAccountId,
            string userIdentifier,
            double elapsedTimeToCreation,
            DateTime creationDate,
            string validatorUserIdentifier,
            string environmentSetupUserIdentifier,
            string typeOfSubscription,
            string status)
        {
            Id = id;
            SubscriptionAccountId = subscriptionAccountId;
            UserIdentifier = userIdentifier;
            ElapsedTimeToCreation = elapsedTimeToCreation;
            CreationDate = creationDate;
            ValidatorUserIdentifier = validatorUserIdentifier;
            EnvironmentSetupUserIdentifier = environmentSetupUserIdentifier;
            TypeOfSubscription = typeOfSubscription;
            Status = status;
        }
    }
}
