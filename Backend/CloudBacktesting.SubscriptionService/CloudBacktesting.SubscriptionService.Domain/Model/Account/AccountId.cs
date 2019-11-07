using Akkatecture.Core;
using Akkatecture.ValueObjects;
using Newtonsoft.Json;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Subscription
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class AccountId : Identity<AccountId>
    {
        public AccountId(string value) : base(value)
        {

        }
    }
}
