using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccounts
{
    //public class SubscriptionAccountsQueryHandler : IQuerySubscriptionAccounts
    //{
    //    private readonly ActorRefProvider<SubscriptionAccountsStorageHandler> _subscriptionAccountsStorageHandler;

    //    public SubscriptionAccountsQueryHandler(
    //        ActorRefProvider<SubscriptionAccountsStorageHandler> subscriptionAccountsStorageHandler)
    //    {
    //        _subscriptionAccountsStorageHandler = subscriptionAccountsStorageHandler;
    //    }

    //    public async Task<SubscriptionAccountsProjection> Find(SubscriptionAccountId id)
    //    {
    //        var query = new GetSubscriptionAccountsQuery();

    //        var result = await _subscriptionAccountsStorageHandler.Ask<List<SubscriptionAccountsProjection>>(query);

    //        var readModel = result.SingleOrDefault(x => x.Id == id);

    //        return readModel;
    //    }

    //    public async Task<IEnumerable<SubscriptionAccountsProjection>> FindAll()
    //    {
    //        var query = new GetSubscriptionAccountsQuery();

    //        var result = await _subscriptionAccountsStorageHandler.Ask<List<SubscriptionAccountsProjection>>(query);

    //        return result;
    //    }
    //}

    //public class GetSubscriptionAccountsQuery
    //{

    //}
}
