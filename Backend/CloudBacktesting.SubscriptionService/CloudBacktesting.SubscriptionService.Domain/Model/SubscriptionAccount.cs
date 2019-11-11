using Akkatecture.Aggregates;
using Akkatecture.Specifications.Provided;
using CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount.Commands;
using CloudBacktesting.SubscriptionService.Domain.Model.Events;

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
            //This spec is part of Akkatecture
            var spec = new AggregateIsNewSpecification();
            if(spec.IsSatisfiedBy(this))
            {
                var aggregateEvent = new SubscriptionCreatedEvent(command.SubscriptionState);
                Emit(aggregateEvent);
            }

            return true;
        }
    }
}
