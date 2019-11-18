
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccounts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountQuery
{
    public interface IQuerySubscriptionAccounts
    {
        Task<SubscriptionAccountsProjection> Find(SubscriptionAccountId id);
        Task<IEnumerable<SubscriptionAccountsProjection>> FindAll();
    }
}
