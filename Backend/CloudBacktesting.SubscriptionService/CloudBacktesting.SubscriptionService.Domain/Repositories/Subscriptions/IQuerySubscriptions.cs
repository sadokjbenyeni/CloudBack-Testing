using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.Subscriptions
{
    public interface IQuerySubscriptions
    {
        Task<SubscriptionsProjection> Find(SubscriptionId id, string userIdentifier);
        Task<IEnumerable<SubscriptionsProjection>> FindByUserId(string userIdentifier);
        Task<IEnumerable<SubscriptionsProjection>> FindAll();
    }
}
