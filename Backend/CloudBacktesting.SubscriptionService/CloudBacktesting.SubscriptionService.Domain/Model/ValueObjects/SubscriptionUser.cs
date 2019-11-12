using Akkatecture.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects
{
    public class SubscriptionUser : SingleValueObject<string>
    {
        public SubscriptionUser(string value) : base(value)
        {
        }
    }
}
