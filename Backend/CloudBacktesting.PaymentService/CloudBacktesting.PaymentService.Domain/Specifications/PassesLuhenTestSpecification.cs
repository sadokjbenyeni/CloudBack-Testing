using EventFlow.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Specifications
{
    public class PassesLuhenTestSpecification : Specification<string>
    {
        public enum CreditCardTypeType
        {
            Visa,
            MasterCard,
            Discover,
            Amex,
            Other
        }

        private const string cardRegex = "^(?:(?<Visa>4\\d{3})|(?<MasterCard>5[1-5]\\d{2})|(?<Discover>6011)|(?<DinersClub>(?:3[68]\\d{2})|(?:30[0-5]\\d))|(?<Amex>3[47]\\d{2}))([ -]?)(?(DinersClub)(?:\\d{6}\\1\\d{4})|(?(Amex)(?:\\d{6}\\1\\d{5})|(?:\\d{4}\\1\\d{4}\\1\\d{4})))$";
       
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
