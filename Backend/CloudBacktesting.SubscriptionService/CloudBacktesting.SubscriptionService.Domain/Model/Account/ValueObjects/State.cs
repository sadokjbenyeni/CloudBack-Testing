using Akkatecture.ValueObjects;


namespace CloudBacktesting.SubscriptionService.Domain.Model.Subscription.ValueObjects
{
    public class State : SingleValueObject<string>
    {
        public State(string value) : base(value)
        {
        }
    }
}
