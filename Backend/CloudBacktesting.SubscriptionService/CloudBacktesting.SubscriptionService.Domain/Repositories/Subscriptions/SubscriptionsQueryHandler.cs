using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.Subscriptions
{

    //public class SubscriptionsQueryHandler : IQuerySubscriptions
    //{
    //    private readonly ActorRefProvider<SubscriptionsStorageHandler> _subscriptionStorageHandler;

    //    public SubscriptionsQueryHandler(
    //        ActorRefProvider<SubscriptionsStorageHandler> subscriptionStorageHandler)
    //    {
    //        _subscriptionStorageHandler = subscriptionStorageHandler;
    //    }

    //    public async Task<SubscriptionsProjection> Find(SubscriptionId id, string userIdentifier)
    //    {
    //        var query = new GetSubscriptionsQuery();

    //        var result = await _subscriptionStorageHandler.Ask<List<SubscriptionsProjection>>(query);

    //        var readModel = result.SingleOrDefault(x => x.Id == id && x.UserIdentifier == userIdentifier);

    //        return readModel;
    //    }

    //    public async Task<IEnumerable<SubscriptionsProjection>> FindAll()
    //    {
    //        var query = new GetSubscriptionsQuery();

    //        var result = await _subscriptionStorageHandler.Ask<List<SubscriptionsProjection>>(query);

    //        return result;
    //    }

    //    public async Task<IEnumerable<SubscriptionsProjection>> FindByUserId(string userIdentifier)
    //    {
    //        var query = new GetSubscriptionsQuery();

    //        var result = await _subscriptionStorageHandler.Ask<List<SubscriptionsProjection>>(query);

    //        var readModel = result.Where(x => x.UserIdentifier == userIdentifier);

    //        return readModel;
    //    }

    //    public class GetSubscriptionsQuery
    //    {

    //    }
    //}
}
