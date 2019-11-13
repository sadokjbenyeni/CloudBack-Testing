using Akkatecture.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Specifications
{
    public class SubscriptionUserIsNotNullSpecification : Specification<SubscriptionAccountState>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(SubscriptionAccountState state)
        {
            if (state.SubscriptionUser != null)
            {
                yield return "Subscription user is null";
            }
        }
    }
}
