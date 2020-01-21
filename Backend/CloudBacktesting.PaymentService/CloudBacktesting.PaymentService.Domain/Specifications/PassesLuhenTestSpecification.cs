using EventFlow.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Specifications
{
    public class PassesLuhenTestSpecification : Specification<string>
    {
        public new bool IsSatisfiedBy(string cardNumber)
        {
            cardNumber = cardNumber.Replace("-", "").Replace(" ", "");

            int[] digits = new int[cardNumber.Length];
            for (int len = 0; len < cardNumber.Length; len++)
            {
                digits[len] = Int32.Parse(cardNumber.Substring(len, 1));
            }

            int sum = 0;
            bool alt = false;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int curDigit = digits[i];
                if (alt)
                {
                    curDigit *= 2;
                    if (curDigit > 9)
                    {
                        curDigit -= 9;
                    }
                }
                sum += curDigit;
                alt = !alt;
            }

            return sum % 10 == 0;
        }

        protected override IEnumerable<string> IsNotSatisfiedBecause(string cardNumber)
        {
            if (IsSatisfiedBy(cardNumber) is false)
            {
                yield return string.Format("{0} is not valid by Luhen algorithm", cardNumber);
            }
        }
    }
}
