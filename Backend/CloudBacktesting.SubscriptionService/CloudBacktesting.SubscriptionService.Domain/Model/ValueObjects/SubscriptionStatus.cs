using Akkatecture.ValueObjects;


namespace CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects
{
    public class SubscriptionStatus : SingleValueObject<string>
    {
        public SubscriptionStatus(string value) : base(value)
        {
        }
    }
}
