using EventFlow.Specifications;
using System.Collections.Generic;

namespace CloudBacktesting.SubscriptionService.Domain.Specifications
{
    public class IsNullOrEmptyAccountIdSpecification : Specification<string>
    {

        protected override IEnumerable<string> IsNotSatisfiedBecause(string subscriptionAccountId)
        {
            if (string.IsNullOrEmpty(subscriptionAccountId) is true)
            {
                yield return string.Format("{0} is null or empty", subscriptionAccountId);
            }
        }
    }
}
