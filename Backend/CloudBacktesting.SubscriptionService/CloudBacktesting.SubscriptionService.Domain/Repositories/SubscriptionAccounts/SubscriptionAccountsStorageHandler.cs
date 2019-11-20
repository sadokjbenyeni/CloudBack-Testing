using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionAccountSaga;
using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionAccountSaga.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccounts
{

    //public class SubscriptionAccountsStorageHandler : DomainEventSubscriber,
    //            ISubscribeToAsync<SubscriptionAccountCreationSaga, SubscriptionAccountCreationSagaId, SubscriptionAccountCreationEndedEvent>
    //{
    //    private readonly List<SubscriptionAccountsProjection> _subscriptionAccounts = new List<SubscriptionAccountsProjection>();

    //    public SubscriptionAccountsStorageHandler(/* TODO: Inject DB Driver abstraction */)
    //    {
    //        Receive<GetSubscriptionAccountsQuery>(Handle);
    //    }

    //    public Task HandleAsync(IDomainEvent<SubscriptionAccountCreationSaga, SubscriptionAccountCreationSagaId, SubscriptionAccountCreationEndedEvent> domainEvent)
    //    {
    //        var readModel = new SubscriptionAccountsProjection(domainEvent.AggregateEvent.Id, domainEvent.AggregateEvent.UserIdentifier, domainEvent.AggregateEvent.Elapsed ,domainEvent.AggregateEvent.EndedAt);

    //        _subscriptionAccounts.Add(readModel);

    //        return Task.CompletedTask;
    //        // todo store in db;
    //    }

    //    public bool Handle(GetSubscriptionAccountsQuery query)
    //    {
    //        Sender.Tell(_subscriptionAccounts, Self);
    //        return true;
    //    }
    //}
}
