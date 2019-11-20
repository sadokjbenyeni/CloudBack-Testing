using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccounts
{
    public class SubscriptionAccountReadModel : IReadModel, IAmReadModelFor<SubscriptionAccount, SubscriptionAccountId, SubscriptionAccountCreatedEvent>
    {
        public string Id { get; set; }
        public long? Version { get; set; }
        public string Subscriber { get; set; }
        public DateTime SubscriptionDate { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<SubscriptionAccount, SubscriptionAccountId, SubscriptionAccountCreatedEvent> domainEvent)
        {
            Id = string.IsNullOrEmpty(Id) ? domainEvent.AggregateIdentity.Value : Id;
            Subscriber = domainEvent.AggregateEvent.Subscriber;
            SubscriptionDate = domainEvent.AggregateEvent.SubscriptionDate;
        }
    }
}
