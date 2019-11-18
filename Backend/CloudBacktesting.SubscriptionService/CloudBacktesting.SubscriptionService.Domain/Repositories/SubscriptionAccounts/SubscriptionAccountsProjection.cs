using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccounts
{
    public class SubscriptionAccountsProjection
    {
        public SubscriptionAccountId Id { get; set; }
        public string UserIdentifier { get; set; }
        public double ElapsedTimeToCreation { get; }
        public DateTime CreationDate { get; set; }

        public SubscriptionAccountsProjection(SubscriptionAccountId id, string userIdentifier, double elapsedTimeToCreation, DateTime creationDate)
        {
            Id = id;
            UserIdentifier = userIdentifier;
            ElapsedTimeToCreation = elapsedTimeToCreation;
            CreationDate = creationDate;
        }
    }

}
