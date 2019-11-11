using Akkatecture.Subscribers;
using CloudBacktesting.SubscriptionService.Domain.Model.Events;
using CloudBacktesting.SubscriptionService.Domain.Model;
using Akka.Actor;
using CloudBacktesting.SubscriptionService.Domain.Model.Commands;
using Akkatecture.Aggregates;

namespace CloudBacktesting.SubscriptionService.Domain.Subscribers.SubscriptionProcessingHistory
{
    public class SubscriptionProcessingHistorySubscriber : DomainEventSubscriber, ISubscribeTo<SubscriptionAccount, SubscriptionAccountId, SubscriptionCreatedEvent>
    {
        public IActorRef SubscriptionProcessingHistoryRepository { get; }

        public SubscriptionProcessingHistorySubscriber( IActorRef subscriptionProcessingHistoryRepository)
        {
            SubscriptionProcessingHistoryRepository = subscriptionProcessingHistoryRepository;
        }

        public bool Handle(IDomainEvent<SubscriptionAccount, SubscriptionAccountId, SubscriptionCreatedEvent> domainEvent)
        {
            var command = new CreateSubscriptionCommand(SubscriptionAccountId.New, domainEvent.AggregateEvent.SubscriptionStatus);
            SubscriptionProcessingHistoryRepository.Tell(command);

            return true;
        }
    }
}
