using EventFlow.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Specifications
{
    public class IsNotNullCryptogram : Specification<string>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(string cryptogram)
        {
            if (string.IsNullOrEmpty(cryptogram) is true)
            {
                yield return string.Format("{0} is null or empty", cryptogram);
            }
        }
    }
}
