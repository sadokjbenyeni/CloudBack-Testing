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

        /// <Summary>
        /// this command create a first time the SubscriptionAccount model
        /// this command can be call only one time by user
        /// <Summary>
        public bool Execute(CreateSubscriptionAccountCommand command) 
        {
            var spec = new AggregateIsNewSpecification();
            if(!spec.IsSatisfiedBy(this))
            {
                return false;
            }
            var aggregateEvent = new SubscriptionAccountCreatedEvent(command.UserIdentifier, DateTime.UtcNow);
            Emit(aggregateEvent);               
            return true;
        }

        /// <Summary>
        /// This command create a new subscription for one user
        /// <Summary>
        public bool Execute(CreateSubscriptionCommand command)
        {
            var spec = new AggregateIsNewSpecification();
            if(!spec.IsSatisfiedBy(this))
            {
                return false;
            }
            var aggregateEvent = new SubscriptionCreatedEvent(command.SubscriptionStatus, command.SubscriptionUser, command.SubscriptionType, command.SubscriptionDate);
            Emit(aggregateEvent);                
            return true;
        }
    }
}
