using Akkatecture.Aggregates;
using Akkatecture.Specifications.Provided;
using CloudBacktesting.SubscriptionService.Domain.Model.Events;
using CloudBacktesting.SubscriptionService.Domain.Model.Commands;

namespace CloudBacktesting.SubscriptionService.Domain.Model
{
    public class SubscriptionAccount : AggregateRoot<SubscriptionAccount, SubscriptionAccountId, SubscriptionAccountState>,
        IExecute<CreateSubscriptionCommand>
    {
        public SubscriptionAccount(SubscriptionAccountId aggregateId): base(aggregateId)
        {

        }

        public bool Execute(CreateSubscriptionCommand command)
        {
            var spec = new AggregateIsNewSpecification();
            if(spec.IsSatisfiedBy(this))
            {
                var aggregateEvent = new SubscriptionCreatedEvent(command.SubscriptionStatus, command.SubscriptionUser, command.SubscriptionType, command.SubscriptionDate);
                Emit(aggregateEvent);
            }
            return true;
        }
    }
}
