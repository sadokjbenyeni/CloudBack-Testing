using Akkatecture.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Specifications
{
    public class SubscriptionStatusValueSpecification : Specification<SubscriptionAccountState>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(SubscriptionAccountState state)
        {
            if (state.SubscriptionStatus.Value != "Creation")
            {
                yield return $"'Subscription value is {state.SubscriptionStatus.Value} different from Creation";
            }
        }
    }
}
