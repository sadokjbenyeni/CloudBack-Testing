using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Repositories.SubscriptionProcessingHistory.ReadModels
{
    public class SubscriptionProcessingHistoryReadModel
    {
        public SubscriptionStatus SubscriptionStatus { get; }
        public int SubscriptionProcessing { get; }

        public SubscriptionProcessingHistoryReadModel( SubscriptionStatus subscriptionStatus, int subscriptionProcessing)
        {
            SubscriptionStatus = subscriptionStatus;
            SubscriptionProcessing = subscriptionProcessing;
        }
        
    }
}
