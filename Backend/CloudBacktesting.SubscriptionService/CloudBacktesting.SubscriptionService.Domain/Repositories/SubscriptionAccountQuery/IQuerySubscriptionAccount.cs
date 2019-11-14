using Akkatecture.Aggregates;
using Akkatecture.Subscribers;
using CloudBacktesting.SubscriptionService.Domain.Model;
using CloudBacktesting.SubscriptionService.Domain.Model.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountQuery
{
    public interface IQuerySubscriptionAccount
    {
        Task<SubscriptionAccountProjection> Find(string id);
        Task<IEnumerable<SubscriptionAccountProjection>> FindAll();
    }

    public class QuerySubscriptionAccount : IQuerySubscriptionAccount
    {
        public Task<SubscriptionAccountProjection> Find(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubscriptionAccountProjection>> FindAll()
        {
            throw new NotImplementedException();
        }
    }

    public class SubscriptionAccountProjection
    {
        public string Id {get; set;}
        public string UserIdentifier {get;set;}
        public DateTime CreationDate {get;set;}
        public DateTime? LastUpdateDate { get; set; }

    }

    public class SubscriptionAccountStorageHandler : DomainEventSubscriber,
                ISubscribeToAsync<SubscriptionAccount, SubscriptionAccountId, SubscriptionAccountCreatedEvent>
    {

        public SubscriptionAccountStorageHandler(/* TODO: Inject DB Driver abstraction */)
        {

        }

        public Task HandleAsync(IDomainEvent<SubscriptionAccount, SubscriptionAccountId, SubscriptionAccountCreatedEvent> domainEvent)
        {
            if (domainEvent is null)
            {
                throw new ArgumentNullException(nameof(domainEvent));
            }

            var projection = new SubscriptionAccountProjection()
            {
                Id = domainEvent.AggregateIdentity.Value,
                CreationDate = domainEvent.AggregateEvent.SubscriptionDate.Values,
                LastUpdateDate = null,  
                UserIdentifier = domainEvent.AggregateEvent.SubscriptionUser.Value
            };
            // todo store in db;
        }
    }

    public class SubscriptionStorageHandler : DomainEventSubscriber,
                ISubscribeToAsync<SubscriptionAccount, SubscriptionAccountId, SubscriptionCreatedEvent>
    {
        public Task HandleAsync(Akkatecture.Aggregates.IDomainEvent<SubscriptionAccount, SubscriptionAccountId, SubscriptionCreatedEvent> domainEvent)
        {
            throw new NotImplementedException();
        }
    }

    public interface IQuerySubscription
    {
        Task<SubscriptionProjection> Find(string id, string userIdentifier);
        Task<IEnumerable<SubscriptionProjection>> FindByUserId(string userIdentifier);
        Task<IEnumerable<SubscriptionProjection>> FindAll();
    }

    public class QuerySubscription : IQuerySubscription
    {
        public Task<SubscriptionProjection> Find(string id, string userIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubscriptionProjection>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubscriptionProjection>> FindByUserId(string userIdentifier)
        {
            throw new NotImplementedException();
        }
    }

    public class SubscriptionProjection
    {
        public string Id { get; set; }
        public string SubscriptionAccountId { get; set; }
        public string UserIdentifier { get; set; }
        public DateTime CreationDate {get;set;}
        public DateTime LastUpdateDate { get; set; }
        public string ValidatorUserIdentifier { get; set; }
        public string EnvironmentSetupUserIdentifier { get; set; }
        public string TypeOfSubscription {get;set;}
        public string Status {get;set;}
    }
}
