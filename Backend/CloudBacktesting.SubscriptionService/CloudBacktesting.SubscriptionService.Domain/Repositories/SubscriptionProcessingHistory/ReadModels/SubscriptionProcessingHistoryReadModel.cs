using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Repositories.SubscriptionProcessingHistory.ReadModels
{
    public class SubscriptionProcessingHistoryReadModel
    {
        public SubscriptionStatus SubscriptionStatus { get; }
        public SubscriptionUser SubscriptionUser { get; }
        public SubscriptionType SubscriptionType { get; }
        public SubscriptionDate SubscriptionDate { get; }
        public int SubscriptionProcessing { get; }

        public SubscriptionProcessingHistoryReadModel( SubscriptionStatus subscriptionStatus, SubscriptionUser subscriptionUser, SubscriptionType subscriptionType, SubscriptionDate subscriptionDate, int subscriptionProcessing)
        {
            SubscriptionStatus = subscriptionStatus;
            SubscriptionUser = subscriptionUser;
            SubscriptionType = subscriptionType;
            SubscriptionDate = subscriptionDate;
            SubscriptionProcessing = subscriptionProcessing;
        }
        
    }
}
